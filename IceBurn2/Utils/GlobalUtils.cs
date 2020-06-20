﻿using System.Globalization;
using System.Linq;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC.SDKBase;

namespace IceBurn.Utils
{
    class GlobalUtils
    {
        // нужные переменные сюда
        public static bool Fly = false;
        public static bool ESP = false;
        public static int flySpeed = 5;
        public static float brightness = 1f;
        public static int walkSpeed = 2;
        public static int cameraFOV = 65;
        public static Vector3 Gravity = Physics.gravity;

        // Anti Crash preset
        public static int max_polygons = 500000;
        public static int max_particles = 30000;
        public static int max_particle_sys = 20;
        public static int max_meshes = 10;

        public static void UpdatePlayerSpeed()
        {
            if (VRCPlayer.field_Internal_Static_VRCPlayer_0 == null)
                return;
            LocomotionInputController componentInChildren = PlayerWrapper.GetCurrentPlayer().GetComponentInChildren<LocomotionInputController>();
            if (componentInChildren != null)
            {
                componentInChildren.runSpeed = walkSpeed;
                componentInChildren.walkSpeed = walkSpeed;
                componentInChildren.strafeSpeed = walkSpeed;
            }
        }

        // телепорт в точку на экране
        public static void RayTeleport()
        {
            Ray ray = new Ray(Wrapper.GetPlayerCamera().transform.position, Wrapper.GetPlayerCamera().transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            if (hits.Length > 0)
            {
                RaycastHit raycastHit = hits[0];
                var thisPlayer = PlayerWrapper.GetCurrentPlayer();
                thisPlayer.transform.position = raycastHit.point;
            }
        }

        public static void ToggleColliders(bool Toggle)
        {
            Collider[] array = UnityEngine.Object.FindObjectsOfType<Collider>();
            Component component = PlayerWrapper.GetCurrentPlayer().GetComponents(Il2CppType.Of<Collider>()).FirstOrDefault<Component>();

            for (int i = 0; i < array.Length; i++)
            {
                Collider collider = array[i];
                bool flag = collider.GetComponent<PlayerSelector>() != null || collider.GetComponent<VRC_Pickup>() != null || collider.GetComponent<QuickMenu>() != null || collider.GetComponent<VRCStation>() != null || collider.GetComponent<VRC_AvatarPedestal>() != null; //ебать какая длинная строка

                if (!flag && collider != component)
                {
                    collider.enabled = Toggle;
                }
            }
        }
    }
}
