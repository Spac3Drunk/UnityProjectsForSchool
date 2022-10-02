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
        this.pos.y = defaultPos.y + (float)(Mathf.Sin(Mathf.PI*timePos)*(-0.08*timePos*(timePos-12)));
        this.pos.z = defaultPos.z + (float)(Mathf.Cos(Mathf.PI*timePos)*(-0.08*timePos*(timePos-12)));
    }
}

namespace Controllers {
    public class streamOfCube : MonoBehaviour{
        private Transform streamHolder;
        [SerializeField] private GameObject cubePrefab = default;
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
            Vector3 streamStartPoint = new Vector3(-6, 1, 0);
            float streamSpeed = 0.02f;
            int objNb = 1;
            float inputTimer = 0.08f;
            float tmpTimer = 0.0f;
            while (true) {
                yield return new WaitForSeconds(0.01f);

                if (Input.GetKey("z")){
                    if (tmpTimer < 0){
                        objNb++;
                        tmpTimer = inputTimer;
                    }
                }
                if (Input.GetKey("s") && objNb > 0){
                    if (tmpTimer < 0){
                        objNb--;
                        tmpTimer = inputTimer;
                    }
                }
                if (Input.GetKey("d")){
                    if (tmpTimer < 0){
                        streamSpeed = streamSpeed*1.1f;
                        tmpTimer = inputTimer;
                    }
                }
                if (Input.GetKey("q")){
                    if (tmpTimer < 0){
                        streamSpeed = streamSpeed/1.1f;
                        tmpTimer = inputTimer;
                    }
                }

                if (tmpTimer >= 0){
                    tmpTimer -= Time.deltaTime;
                }

                if (allStreamObj.Count < objNb){
                    allStreamObj.Add(new stream_Obj(streamStartPoint));
                } else if (allStreamObj.Count > objNb){
                    allStreamObj.RemoveAt(0);
                }

                //Debug.Log(streamSpeed);

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