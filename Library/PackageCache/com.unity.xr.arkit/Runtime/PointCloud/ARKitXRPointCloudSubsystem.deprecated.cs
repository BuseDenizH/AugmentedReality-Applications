using System;
using System.Runtime.InteropServices;
using AOT;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Scripting;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARKit
{
    /// <summary>
    /// The ARKit implementation of the <c>XRPointCloudSubsystem</c>. Do not create this directly. Use the <c>SubsystemManager</c> instead.
    /// </summary>
    [Obsolete("ARKitXRPointCloudSubsystem has been deprecated in Apple ARKit XR Plug-in 6.0. Use ARKitPointCloudSubsystem instead (UnityUpgradable) -> UnityEngine.XR.ARKit.ARKitPointCloudSubsystem", true)]
    public sealed class ARKitXRPointCloudSubsystem : XRPointCloudSubsystem
    {
        class ARKitProvider : Provider
        {
#if UNITY_XR_ARKIT_LOADER_ENABLED
            [DllImport("__Internal")]
            static extern void UnityARKit_pointCloud_start();

            [DllImport("__Internal")]
            static extern void UnityARKit_pointCloud_stop();

            [DllImport("__Internal")]
            static extern void UnityARKit_pointCloud_destroy();

            [DllImport("__Internal")]
            static extern unsafe void* UnityARKit_pointCloud_acquireChanges(
                out void* addedPtr, out int addedLength,
                out void* updatedPtr, out int updatedLength,
                out void* removedPtr, out int removedLength,
                out int elementSize);

            [DllImport("__Internal")]
            static extern unsafe void UnityARKit_pointCloud_releaseChanges(
                void* changes);

            [DllImport("__Internal")]
            static extern unsafe void* UnityARKit_pointCloud_acquirePointCloud(
                TrackableId trackableId,
                out void* positionsPtr, out void* identifiersPtr, out int numPoints);

            [DllImport("__Internal")]
            static extern unsafe void UnityARKit_pointCloud_releasePointCloud(
                void* pointCloud);
#else
            static readonly string k_ExceptionMsg = "Apple ARKit XR Plug-in Provider not enabled in project settings.";

            static void UnityARKit_pointCloud_start()
            {
                throw new System.NotImplementedException(k_ExceptionMsg);
            }

            static void UnityARKit_pointCloud_stop()
            {
                throw new System.NotImplementedException(k_ExceptionMsg);
            }

            static void UnityARKit_pointCloud_destroy()
            {
                throw new System.NotImplementedException(k_ExceptionMsg);
            }

            static unsafe void* UnityARKit_pointCloud_acquireChanges(
                out void* addedPtr, out int addedLength,
                out void* updatedPtr, out int updatedLength,
                out void* removedPtr, out int removedLength,
                out int elementSize)
            {
                throw new System.NotImplementedException(k_ExceptionMsg);
            }

            static unsafe void UnityARKit_pointCloud_releaseChanges(
                void* changes)
            {
                throw new System.NotImplementedException(k_ExceptionMsg);
            }

            static unsafe void* UnityARKit_pointCloud_acquirePointCloud(
                TrackableId trackableId,
                out void* positionsPtr, out void* identifiersPtr, out int numPoints)
            {
                throw new System.NotImplementedException(k_ExceptionMsg);
            }

            static unsafe void UnityARKit_pointCloud_releasePointCloud(
                void* pointCloud)
            {
                throw new System.NotImplementedException(k_ExceptionMsg);
            }

#endif
            public override unsafe TrackableChanges<XRPointCloud> GetChanges(
                XRPointCloud defaultPointCloud,
                Allocator allocator)
            {
                var context = UnityARKit_pointCloud_acquireChanges(
                    out var addedPtr, out var addedLength,
                    out var updatedPtr, out var updatedLength,
                    out var removedPtr, out var removedLength,
                    out var elementSize);

                try
                {
                    return new TrackableChanges<XRPointCloud>(
                        addedPtr, addedLength,
                        updatedPtr, updatedLength,
                        removedPtr, removedLength,
                        defaultPointCloud, elementSize,
                        allocator);
                }
                finally
                {
                    UnityARKit_pointCloud_releaseChanges(context);
                }
            }

            public override void Start() => UnityARKit_pointCloud_start();

            public override void Stop() => UnityARKit_pointCloud_stop();

            public override void Destroy() => UnityARKit_pointCloud_destroy();

            public override unsafe XRPointCloudData GetPointCloudData(
                TrackableId trackableId,
                Allocator allocator)
            {
                var pointCloud = UnityARKit_pointCloud_acquirePointCloud(
                    trackableId,
                    out var positionsPtr, out var identifiersPtr, out var numPoints);

                try
                {
                    var positions = new NativeArray<Vector3>(numPoints, allocator);
                    var positionsHandle = new TransformPositionsJob
                    {
                        positionsIn = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Quaternion>(positionsPtr, numPoints, Allocator.None),
                        positionsOut = positions
                    }.Schedule(numPoints, 32);

                    var identifiers = new NativeArray<ulong>(numPoints, allocator);
                    identifiers.CopyFrom(NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ulong>(identifiersPtr, numPoints, Allocator.None));

                    positionsHandle.Complete();
                    return new XRPointCloudData
                    {
                        positions = positions,
                        identifiers = identifiers
                    };
                }
                finally
                {
                    UnityARKit_pointCloud_releasePointCloud(pointCloud);
                }
            }
        }

        struct TransformPositionsJob : IJobParallelFor
        {
            [ReadOnly]
            public NativeArray<Quaternion> positionsIn;

            [WriteOnly]
            public NativeArray<Vector3> positionsOut;

            public void Execute(int index)
            {
                positionsOut[index] = new Vector3(
                     positionsIn[index].x,
                     positionsIn[index].y,
                    -positionsIn[index].z);
            }
        }
    }
}
