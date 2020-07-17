using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroGun : MonoBehaviour
{
    private int type = 0;
    [SerializeField] Transform firePoint; // shoot point
    [SerializeField] GameObject bullet;
    private GameObject armBone;
    // Start is called before the first frame update
    void Start()
    {
        armBone = GameObject.Find("bone_5");
        transform.Rotate(0, 0, armBone.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {

        //print(Camera.main.ScreenToWorldPoint(firePoint.transform.position));
        //transform.Rotate(0, 0, GameObject.Find("bone_5").transform.rotation.eulerAngles.z);
        //Debug.DrawRay(transform.TransformPoint(firePoint.transform.position), new Vector3(0,1,0), Color.red);

        /* var mousePosition = Input.mousePosition;
         //mousePosition.z = transform.position.z - Camera.main.transform.position.z; // это только для перспективной камеры необходимо
         mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
         var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
         transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);//немного магии на последок*/

    }

    public void Shoot()
    {
        Instantiate(bullet, firePoint.position, armBone.transform.rotation);
        //print("PRot" + player.transform.rotation + "FP rot" + firePoint.rotation);
    }
}
