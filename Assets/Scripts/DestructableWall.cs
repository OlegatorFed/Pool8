using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    private class DestroyWallAction : IRewindableAction
    {
        private GameObject wall;
        
        public DestroyWallAction(GameObject wall)
        {
            this.wall = wall;
        }
        
        public void Dispatch()
        {
            wall.SetActive(false);
        }

        public void Rewind()
        {
            wall.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Input.GetKey(KeyCode.R))
            return;
        
        if (collision.gameObject.tag == "Player")
        {
            RewindManager.instance.Dispatch(new DestroyWallAction(gameObject));
        }

    }
}
