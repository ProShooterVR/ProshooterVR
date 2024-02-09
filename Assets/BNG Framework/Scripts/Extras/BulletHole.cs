using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG {

    /// <summary>
    /// A simple decal with random scale and rotation
    /// </summary>
    public class BulletHole : MonoBehaviour {

        public float MaxScale = 1f;
        public float MinScale = 0.75f;

        public bool RandomYRotation = true;

        public float DestroyTime;

        // Start is called before the first frame update
        void Start() {
            transform.localScale = Vector3.one * Random.Range(0.75f, 1.5f);

            if ( RandomYRotation) {
            }



            StartCoroutine(DestroySelf());
        }

        public void TryAttachTo(Collider col) {
            if (transformIsEqualScale(col.transform)) {
            }
            // No need to parent if static collider
            else if (col.gameObject.isStatic) {
            }
            // Malformed collider (non-equal proportions)
            // Just destroy the decal quickly
            else {
                // BulletHoleDecal.parent = col.transform;
            }
        }

        // Are all scales equal? Ex : 1, 1, 1
        bool transformIsEqualScale(Transform theTransform) {
            return theTransform.localScale.x == theTransform.localScale.y && theTransform.localScale.x == theTransform.localScale.z;
        }

        IEnumerator DestroySelf() {

            yield return new WaitForSeconds(DestroyTime);
            transform.parent = null;
            GameObject.Destroy(this.gameObject);

            
        }
    }
}

