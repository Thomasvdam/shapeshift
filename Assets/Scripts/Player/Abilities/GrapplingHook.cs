﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class GrapplingHook : MonoBehaviour
    {
        public float Length = 4;

        [HideInInspector]
        public bool IsEnabled
        {
            get { return _points.Any(); }
        }

        private readonly List<GameObject> _points = new List<GameObject>();
        private LineRenderer _line;
        private GameObject _grapple;
        private GameObject _previousGrapple;
        private float _previousDistance = -1;
        private DistanceJoint2D _joint;

		private Vector3 grappleDirection;
		private int pullForce = 1000;

        void Start()
        {
            _line = new GameObject("Line").AddComponent<LineRenderer>();
            _line.SetVertexCount(2);
            _line.SetWidth(.025f, .025f);
            _line.gameObject.SetActive(false);
            _line.SetColors(Color.black, Color.black);
            _line.GetComponent<Renderer>().material.color = Color.black;

            _grapple = new GameObject("Grapple");
            _grapple.AddComponent<CircleCollider2D>().radius = .1f;
            _grapple.AddComponent<Rigidbody2D>();
            _grapple.GetComponent<Rigidbody2D>().isKinematic = true;
			_grapple.transform.position = new Vector3 (100, 100, 100); // Move grapple far away

            _previousGrapple = (GameObject)Instantiate(_grapple);
            _previousGrapple.name = "Previous Grapple";

            _joint = gameObject.AddComponent<DistanceJoint2D>();
            _joint.enabled = false;
        }

        void Update()
        {
            if (IsEnabled) UpdateGrapple();
            else CheckForGrapple();
        }
			
        private void CheckForGrapple()
        {
            if (Input.GetKeyDown("joystick 1 button 5"))
            {
				// Get joystick direction. TODO get this from other object/script
				float x = Input.GetAxisRaw ("joystick 1 X axis");
				float y = Input.GetAxisRaw ("joystick 1 Y axis");
				grappleDirection = new Vector3 (x, -y, 0.0f);

				var grapplePoint = transform.position + grappleDirection.normalized * Length;
				var hit = Physics2D.Linecast(transform.position, grapplePoint, ~(1 << 8)); // All layers except 8?
                var distance = Vector3.Distance(transform.position, hit.point);
                if (hit.collider != null && distance <= Length)
                {
                    _line.SetVertexCount(2);
                    _line.SetPosition(0, hit.point);
                    _line.SetPosition(1, transform.position);
                    _line.gameObject.SetActive(true);

                    _points.Add(CreateGrapplePoint(hit));

                    _grapple.transform.position = hit.point;
                    SetParent(_grapple.transform, hit.collider.transform);

                    _joint.enabled = true;
                    _joint.connectedBody = _grapple.GetComponent<Rigidbody2D>();
                    _joint.distance = Vector3.Distance(hit.point, transform.position);
                    _joint.maxDistanceOnly = true;
                }
            }
        }

        private GameObject CreateGrapplePoint(RaycastHit2D hit)
        {
            var p = new GameObject("GrapplePoint");
            SetParent(p.transform, hit.collider.transform);
            p.transform.position = hit.point;
            return p;
        }

        private void UpdateGrapple()
        {
            UpdateLineDrawing();

            var hit = Physics2D.Linecast(transform.position, _grapple.transform.position, ~(1 << 8));
            var hitPrev = Physics2D.Linecast(transform.position, _previousGrapple.transform.position, ~(1 << 8));

            if (hit.collider.gameObject != _grapple && hit.collider.gameObject != _previousGrapple)
            {
                // if you lose line of sight on the grappling hook, then add a new point to wrap around

                _points.Add(CreateGrapplePoint(hit));

                UpdateLineDrawing();

                _previousGrapple.transform.position = _grapple.transform.position;
                SetParent(_previousGrapple.transform, _grapple.transform.parent);
                _grapple.transform.position = hit.point;
                SetParent(_grapple.transform, hit.collider.transform);
                _previousDistance = -1;

                _joint.distance -= Vector3.Distance(_grapple.transform.position, _previousGrapple.transform.position);
            }
            else if (Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown("joystick 1 button 1"))
            {
                // if you retract the grappling hook

                // jump off
                if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < _grapple.transform.position.y)
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 3);

                RetractRope();
            } else if(Input.GetKeyDown("joystick 1 button 4")) {
				//maurits TODO
				// On key: pull grappling hook target.
				//Debug.Log(hit);
				if (_points.Count <= 1) {
					Vector3 pullDirection = new Vector3 (transform.position.x - _points[0].transform.position.x, transform.position.y - _points[0].transform.position.y, 0.0f);
					hit.rigidbody.AddForce (pullDirection.normalized * pullForce);
					Debug.Log (pullDirection);
				} else {
					Vector3 pullDirection = new Vector3 (_points[1].transform.position.x - _points[0].transform.position.x, _points[1].transform.position.y - _points[0].transform.position.y, 0.0f);
					hit.rigidbody.AddForce (pullDirection.normalized * pullForce);
					Debug.Log (pullDirection);
				}

			} else if (Vector3.Distance(_grapple.transform.position, _previousGrapple.transform.position) <= .1f)
            {
                RemoveLastCollider();
            }
            else
            {
                // always update the last points in the line to track player

                _line.SetPosition(_points.Count, transform.position);
                GetComponent<Rigidbody2D>().AddForce(Vector3.right * Input.GetAxisRaw("joystick 1 X axis") * 25);
                _joint.distance -= Input.GetAxisRaw("joystick 1 Y axis") * Time.deltaTime;

                // if you can see previous point then unroll back to that point
                if (hitPrev.collider != null && hitPrev.transform == _previousGrapple.transform)
                    RemoveLastCollider();
            }

            UpdateDistance();
        }

        private void RetractRope()
        {
            _joint.enabled = false;
            _line.gameObject.SetActive(false);
            _points.ForEach(Destroy);
            _points.Clear();
            _grapple.transform.position = new Vector3(0, 0, -1);
            _previousGrapple.transform.position = new Vector3(0, 0, -1);
            _previousDistance = -1;
        }

        private void RemoveLastCollider()
        {
            if (_points.Count > 1)
            {
                Destroy(_points[_points.Count - 1]);
                _points.RemoveAt(_points.Count - 1);

                UpdateLineDrawing();

                _joint.distance += Vector3.Distance(_grapple.transform.position, _previousGrapple.transform.position);
                _grapple.transform.position = _previousGrapple.transform.position;
                SetParent(_grapple.transform, _previousGrapple.transform.parent);
            }

            if (_points.Count > 1)
                _previousGrapple.transform.position = _points.ElementAt(_points.Count - 2).transform.position;
            else
                _previousGrapple.transform.position = new Vector3(0, 0, -1);

            _previousDistance = -1;
        }

        private void UpdateLineDrawing()
        {
            _line.SetVertexCount(_points.Count + 1);
            for (var i = 0; i < _points.Count; i++)
                _line.SetPosition(i, _points[i].transform.position);
            _line.SetPosition(_points.Count, transform.position);
        }

        private void UpdateDistance()
        {
            if(_points.Count == 0) return;

            var distance = 0f;

            for (var i = 1; i < _points.Count; i++)
                distance += Vector3.Distance(_points[i - 1].transform.position, _points[i].transform.position);
            distance += Vector3.Distance(_points[_points.Count - 1].transform.position, transform.position);

            if (_previousDistance > 0)
                _joint.distance += _previousDistance - distance;

            _previousDistance = distance;

            if(distance > Length) RetractRope();
        }

        private void SetParent(Transform child, Transform parent)
        {
            child.SetParent(parent);
            if (parent != null)
                child.localScale = new Vector3(1 / parent.localScale.x, 1 / parent.localScale.y, 1 / parent.localScale.z);
        }
    }
}