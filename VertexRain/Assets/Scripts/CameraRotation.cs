using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;

    async void Start()
    {
        await CameraLoop();
    }

    private async Task CameraLoop()
    {
        float i = 0;
        float moment = 1f;

        while (true)
        {
            transform.rotation = Quaternion.Euler(i, 0f, 0f);

            await new WaitForEndOfFrame();
            i += (_speed * moment);

            if (i < -90f || 90f < i) moment = moment * -1f;
        }
    }
}
