using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
public static class Extension
{
    //Reve
    /// <summary>
    /// Reset Global Position to (0,0,0)
    /// </summary>
    /// <param name="target"></param>
    public static void ResetGlobalPosition(this Transform target) => target.position = Vector3.zero;
    /// <summary>
    /// Change Global Position x Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="x"></param>
    public static void SetGlobalPosX(this Transform target, float x) => target.position = new Vector3(x, target.position.y, target.position.z);
    /// <summary>
    /// Change Global Position y Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="y"></param>
    public static void SetGlobalPosY(this Transform target, float y) => target.position = new Vector3(target.position.x, y, target.position.z);
    /// <summary>
    /// Change Global Position z Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="z"></param>
    public static void SetGlobalPosZ(this Transform target, float z) => target.position = new Vector3(target.position.x, target.position.y, z);
    /// <summary>
    /// Reset rotation to Quaternion.identity
    /// </summary>
    /// <param name="target"></param>
    public static void ResetGlobalRotation(this Transform target) => target.rotation = Quaternion.identity;
    /// <summary>
    /// Reset Local Position to (0,0,0)
    /// </summary>
    /// <param name="target"></param>
    public static void ResetLocalPosition(this Transform target) => target.localPosition = Vector3.zero;
    /// <summary>
    /// Change Local Position x Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="x"></param>
    public static void SetLocalPosX(this Transform target, float x) => target.localPosition = new Vector3(x, target.localPosition.y, target.localPosition.z);
    /// <summary>
    /// Change Local Position y Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="y"></param>
    public static void SetLocalPosY(this Transform target, float y) => target.localPosition = new Vector3(target.localPosition.x, y, target.localPosition.z);
    /// <summary>
    /// Change Local Position z Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="z"></param>
    public static void SetLocalPosZ(this Transform target, float z) => target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, z);
    /// <summary>
    /// Reset local rotation to  Quaternion.identity
    /// </summary>
    /// <param name="targe"></param>
    public static void ResetLocalRotation(this Transform targe) => targe.localRotation = Quaternion.identity;
    /// <summary>
    /// Reset Scale to (1,1,1)
    /// </summary>
    /// <param name="target"></param>
    public static void ResetScale(this Transform target) => target.localScale = Vector3.one;
    /// <summary>
    /// Scale to (0,0,0)
    /// </summary>
    /// <param name="target"></param>
    public static void ZeroScale(this Transform target) => target.localScale = Vector3.one;
    /// <summary>
    /// Change Local Scale x Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="x"></param>
    public static void SetLocalScaleX(this Transform target, float x) => target.localScale = new Vector3(x, target.localScale.y, target.localScale.z);
    /// <summary>
    /// Change Local Scale y Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="y"></param>
    public static void SetLocalScaleY(this Transform target, float y) => target.localScale = new Vector3(target.localScale.x, y, target.localScale.z);
    /// <summary>
    /// Change Local Scale z Axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="z"></param>
    public static void SetLocalScaleZ(this Transform target, float z) => target.localScale = new Vector3(target.localScale.x, target.localScale.y, z);
    /// <summary>
    /// Shuffle the list based on Fisher-Yates algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rnd = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    /// <summary>
    /// Returns the result of a non-clamping linear remapping of value from source range [currentMin, currentMax] to the destination range [newMin, newMax]
    /// </summary>
    /// <param name="value"></param>
    /// <param name="currentMin"></param>
    /// <param name="currentMax"></param>
    /// <param name="newMin"></param>
    /// <param name="newMax"></param>
    /// <returns></returns>
    public static float Remap(this float value, float currentMin, float currentMax, float newMin, float newMax)
    {
        return (value - currentMin) / (currentMax - currentMin) * (newMax - newMin) + newMin;
    }

    public static void DestroyAllChildren(this Transform target)
    {
        for (int i = target.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(target.GetChild(i).gameObject);
        }
    }
    public static void DestroyChildrens(this Transform target, int count)
    {

    }
    public static void DestroyChildrenImmediate(this Transform target)
    {
        for (int i = target.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(target.GetChild(i).gameObject);
        }
    }
    public static void DisableChildrens(this Transform target)
    {
        if (target.childCount == 0) return;
        for (int i = target.childCount - 1; i >= 0; i--)
        {
            target.GetChild(i).gameObject.SetActive(false);
        }
    }
    public static void ChangeParticleColor(this ParticleSystem particle, Color color)
    {
        ParticleSystem.MainModule main = particle.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);
    }
    public static void ChangeParticleColor(this ParticleSystem particle, Color colorFrom, Color colorTo)
    {
        ParticleSystem.MainModule main = particle.main;
        Gradient gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(colorFrom, 0), new GradientColorKey(colorTo, 1) },
            new GradientAlphaKey[] { new GradientAlphaKey(1, 0f), new GradientAlphaKey(1, 1f) });
        main.startColor = new ParticleSystem.MinMaxGradient(gradient);
    }
    public static void ChangeParticleColor(this ParticleSystem particle, Gradient gradient)
    {
        ParticleSystem.MainModule main = particle.main;
        main.startColor = new ParticleSystem.MinMaxGradient(gradient);
    }
    public static void PlayParticleAtPosition(this ParticleSystem particle, Vector3 worldPos)
    {
        if (particle.isPlaying) particle.Stop();
        particle.transform.position = worldPos;
        particle.Play();
    }
    public static string FormatNumber(this float num)
    {
        if (num > 999999999 || num < -999999999)
        {
            return num.ToString("0,,,.####B", CultureInfo.InvariantCulture);
        }

        if (num > 999999 || num < -999999)
        {
            return num.ToString("0,,.###M", CultureInfo.InvariantCulture);
        }

        if (num > 999 || num < -999)
        {
            return num.ToString("0,.##K", CultureInfo.InvariantCulture);
        }

        return num.ToString("0.##", CultureInfo.InvariantCulture);
    }
    /// <summary>
    /// Return nth Fibonacci Number
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int Fibonacci(this int n)
    {
        if (n == 0) return 0;
        else if (n == 1) return 1;
        else
        {
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
    /// <summary>
    /// Convert float time to 00:00:00 format
    /// </summary>
    /// <param name="timeInSecond"></param>
    /// <returns></returns>
    public static string ConvertTime(this float timeInSecond)
    {

        int seconds = (int)(timeInSecond % 60);
        int minutes = (int)(timeInSecond / 60) % 60;
        //int hours = (int)(timeInSecond / 3600) % 24;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void SetEnable(this GameObject gameobject)
    {
        if (gameobject != null)
            gameobject.SetActive(true);
    }
    public static void SetDisable(this GameObject gameobject)
    {
        if (gameobject != null)
            gameobject.SetActive(false);
    }

    public static float InverseLerp(this Vector3 value, Vector3 a, Vector3 b)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }

    public static bool IsNullOrDestroyed(this System.Object obj)
    {

        if (object.ReferenceEquals(obj, null)) return true;

        if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;

        return false;
    }

    public static void DoAfterSeconds(this MonoBehaviour obj, float seconds, Action action)
    {
        if (obj.isActiveAndEnabled)
        {
            obj.StartCoroutine(DoAfterSecondsCO(obj, seconds, action));
        }
    }

    public static IEnumerator DoAfterSecondsCO(MonoBehaviour obje, float seconds, Action action)
    {
        yield return GetWaitForSeconds(seconds);
        action?.Invoke();
    }

    private static readonly Dictionary<float, WaitForSeconds> _waitMap = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWaitForSeconds(float duration)
    {
        if (_waitMap.TryGetValue(duration, out WaitForSeconds waitForSeconds))
        {
            return waitForSeconds;
        }

        _waitMap.Add(duration, new WaitForSeconds(duration));

        return _waitMap[duration];
    }

    public static float ClampAngle(float current, float min, float max)
    {
        if (current < -360f) current += 360f;
        if (current > 360f) current -= 360f;
        return Mathf.Clamp(current, min, max);
    }

    public static Vector3 OPLerp(Vector3 a, Vector3 b, float speed, float deltaTime)
    {
        //float idealDeltaTime = 1f / 60f; // 60 FPS'e gore ideal delta time
        //float normalizedSpeed = speed * (deltaTime / idealDeltaTime); // Speed'i normallestir
        return Vector3.Lerp(a, b, Damp(speed, deltaTime));
    }

    public static float Damp(float lambda, float dt)
    {
        return 1 - Mathf.Exp(-lambda * dt);
    }

    public static Vector3 ToInt(this Vector3 worldPoint)
    {
        worldPoint.x = (int)worldPoint.x;
        worldPoint.y = (int)worldPoint.y;
        worldPoint.z = (int)worldPoint.z;
        return worldPoint;
    }
}
