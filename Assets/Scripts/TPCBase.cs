using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }

        public void RepositionCamera()
        {
            Vector3 fixedCam = mCameraTransform.position;
            Vector3 fixedPlayer = mPlayerTransform.position;
            Vector3 PlayerOffset = new Vector3(fixedPlayer.x, fixedCam.y, fixedPlayer.z);
            RaycastHit hit, hit2;
            
            Vector3 direction = fixedCam - PlayerOffset;
            //Vector3 cameraOffset = new Vector3();
            Vector3 towardsPlayer = PlayerOffset - mCameraTransform.position;

            float controllingValue = 0.0f;
            Vector3 normalizeTowards = towardsPlayer.normalized;
            //Sends a ray from the new player position towards the camera
            if (Physics.Raycast(PlayerOffset, direction, out hit))
            {
                //Vector3 normalizeHit = hit.point - mCameraTransform.position;
                if (hit.point.magnitude < 5.3f)
                {
                    controllingValue = -1f;
                }
                else if (hit.point.magnitude > 5.8f)
                {
                    controllingValue = 1f;
                }
                
                
                Vector3 towardsCam = - mCameraTransform.position + hit.point;
                Vector3 normalizeHit = towardsCam.normalized;
                //Checks if the 2 vectors are facing the same direction
                float dotProduct = Vector3.Dot(towardsCam.normalized, direction.normalized);

                if (dotProduct > 0.999f && hit.point.magnitude < 7.0f)
                {
                    
                    Vector3 cameraOffset = Vector3.Lerp(-towardsCam.normalized, Vector3.zero, controllingValue);
                    mCameraTransform.position = hit.point + cameraOffset;
                }
                else
                {
                    Vector3 cameraOffset = Vector3.Lerp(towardsCam.normalized, Vector3.zero, controllingValue);
                    mCameraTransform.position = hit.point + cameraOffset;
                }
                
                //mCameraTransform.position = new Vector3(hit.point.x, mCameraTransform.position.y, hit.point.z);
                //mCameraTransform.position = Vector3.Lerp(mCameraTransform.position, hit.point + normalizeCamera, Time.deltaTime * smoothFactor);
                Debug.Log("fgdsgdsg");
                Debug.Log(hit.point.magnitude);
            }
                
        }
        public float smoothFactor = 300.0f;
        public abstract void Update();
    }
}


