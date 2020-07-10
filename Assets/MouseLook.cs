using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* var mousePosition = Input.mousePosition;
         //mousePosition.z = transform.position.z - Camera.main.transform.position.z; // это только для перспективной камеры необходимо
         mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
         var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
         //transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);//немного магии на последок
         transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, transform.position.x < mousePosition.x ? -angle : angle), Time.deltaTime);
 */
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0); // переменной записываються координаты мыши по иксу и игрику
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
        transform.position = objPosition; // и собственно объекту записываються координаты

        print(mousePosition);
    }
}
