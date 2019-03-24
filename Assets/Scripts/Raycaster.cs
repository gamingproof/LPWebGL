using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public GoldManager goldManager;
    private PlayerObject objectForBuild;
    private GameObject objectInWorld;
    private bool canBuild;


    void Update()
    {
        if (objectForBuild != null)
        {
            TryForBuild();
            CheckClick();
        }
    }

    public void SetObjectForBuild(PlayerObject o)
    {
        ClearBuilds(true);
        if (!goldManager.TryToBuy(o.Price)) return;
        objectForBuild = o;
        objectInWorld = Instantiate(objectForBuild.Prefab);
        var col = objectInWorld.GetComponentInChildren<Collider>();
        col.enabled = false; // чтобы рейкастом при проверке позиционирования не попадало в сам объект
    }

    /// <summary>
    /// Проверяет можно ли построить в этом месте рейкастом сферой с радиусом объекта и изменяет цвет.
    /// </summary>
    private void TryForBuild()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.SphereCast(ray, objectForBuild.Square, out hit)) //возможно сделать SphereCastAll чтобы заходило прям в уже построенный объект
        {
            if (hit.transform.CompareTag("Ground"))
            {
                objectInWorld.transform.position = hit.point;
                ColorChange(Color.green);
                canBuild = true;
            }
            else
            {
                ColorChange(Color.red);
                canBuild = false;
            }
        }
        else
        {
            ColorChange(Color.red);
            canBuild = false;
        }
    }

    private void CheckClick()
    {
        if (Input.GetMouseButton(0) && canBuild)
        {
            objectInWorld.GetComponentInChildren<Turret>().Enable = true;
            var col = objectInWorld.GetComponentInChildren<Collider>();
            col.enabled = true;
            ClearBuilds(false);
        }
        else if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Escape))
        {
            ClearBuilds();
        }
    }

    private void ClearBuilds(bool destroyObject = true)
    {
        if (destroyObject)
        {
            if (objectForBuild != null) goldManager.AddGold(objectForBuild.Price);
            Destroy(objectInWorld);
        }
        objectInWorld = null;
        objectForBuild = null;
    }

    private void ColorChange(Color color)
    {
        objectInWorld.GetComponentInChildren<Renderer>()?.material.SetColor("_Color", color);
    }
}
