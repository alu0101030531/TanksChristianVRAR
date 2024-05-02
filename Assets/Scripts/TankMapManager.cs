/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

/**
 * Spawns a <see cref="CarBehaviour"/> when a plane is tapped.
 */
public class TankMapManager : MonoBehaviour
{
    public GameObject Tank;
    public GameObject Map;
    public ReticleBehaviour Reticle;
    public DrivingSurfaceManager DrivingSurfaceManager;
    private bool m_instantiated = false;

    private void Update()
    {
        if (!m_instantiated && WasTapped() && Reticle.CurrentPlane != null)
        {
            m_instantiated = true;
            // Spawn our car at the reticle location.
            var tank = GameObject.Instantiate(Tank);
            tank.transform.position = Reticle.transform.position;
            var map = GameObject.Instantiate(Map);
            map.transform.position = Reticle.transform.position;
            DrivingSurfaceManager.LockPlane(Reticle.CurrentPlane);
        }
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
