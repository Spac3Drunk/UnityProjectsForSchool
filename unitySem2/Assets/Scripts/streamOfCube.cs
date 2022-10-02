using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stream_Obj {
    public float timePos = 0f;
    public Vector3 pos;
    public Vector3 defaultPos;

    public stream_Obj(Vector3 position) {
        this.timePos = 0f;
        this.pos = position;
        this.defaultPos = position;
    }

    public void updatePos() { // set the path of the stream objects
        this.pos.x = defaultPos.x + (float)(timePos);
        this.pos.y = defaultPos.y + (float)(Mathf.Sin(Mathf.PI*timePos)*(-0.15*timePos*(timePos-6)));
        this.pos.z = defaultPos.z + (float)(Mathf.Cos(Mathf.PI*timePos)*(-0.15*timePos*(timePos-6)));
    }
}

namespace Controllers {
    public class streamOfCube : MonoBehaviour{
        private Transform streamHolder;
        [SerializeField] private GameObject cubePrefab = default;
        public Vector3 streamStartPoint = new Vector3(-6, 1, 0);
        public float streamSpeed = 0.05f;
        public int objNb = 50;
        public List<stream_Obj> allStreamObj = new List<stream_Obj>();

        private void Awake() {
            streamHolder = transform;
        }

        // Start is called before the first frame update
        void Start(){
            StartCoroutine(UpdateStream());
        }

        // Update is called once per frame
        void Update(){
        
        }

        private IEnumerator UpdateStream() {
            while (true) {
                yield return new WaitForSeconds(0.05f);

                if (allStreamObj.Count < objNb){
                    allStreamObj.Add(new stream_Obj(streamStartPoint));
                } else if (allStreamObj.Count > objNb){
                    allStreamObj.RemoveAt(0);
                }

                for (int i = 0; i < allStreamObj.Count; i++){
                    if (allStreamObj[i].pos.x > -streamStartPoint.x){
                        allStreamObj[i].pos = streamStartPoint;
                        allStreamObj[i].timePos = 0f;
                    } else{
                        allStreamObj[i].timePos += streamSpeed;
                        allStreamObj[i].updatePos();
                    }
                }

                foreach (Transform child in streamHolder) Destroy(child.gameObject);

                for (int i = 0; i < allStreamObj.Count; i++){
                    Instantiate(cubePrefab, allStreamObj[i].pos, Quaternion.identity, streamHolder);
                }
            }
        }
    }
}