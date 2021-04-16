using System;
using Core;
using UnityEngine;

namespace Actors.JellyEffect
{
     public class JellyMesh : BaseMonoBehaviour
     {
          public bool autoInit;
          private Mesh _originalMesh;
          private Mesh _clonedMesh;
          private MeshRenderer _renderer;
          private JellyVertex[] _jellyVertices;
          private Vector3[] _vertexArray;
          private bool _isInitialized;
          
          private Mesh SharedMesh
          {
               get
               {
                    if (_sm == null) _sm = GetComponent<MeshFilter>().sharedMesh;
                    return _sm;
               }
               set => _sm = value;
          }
          private Mesh _sm;
     
          private JellyConfig _config => GameController.Parameters.jelly;

          protected override void ReleaseReferences()
          {
               _sm = null;
               _vertexArray = null;
               _jellyVertices = null;
               _renderer = null;
               _clonedMesh = null;
               _originalMesh = null;
          }

          private void Start()
          {
               if(autoInit) Initialize();
          }

          public void Initialize()
          {
               _isInitialized = true;
               _originalMesh = SharedMesh;
               _clonedMesh = Instantiate(_originalMesh);
               SharedMesh = _clonedMesh;
               _renderer = GetComponent<MeshRenderer>();

               _jellyVertices = new JellyVertex[_clonedMesh.vertices.Length];
               for (var i = 0; i < _clonedMesh.vertices.Length; i++)
                    _jellyVertices[i] = new JellyVertex(i, transform.TransformPoint(_clonedMesh.vertices[i]));
          }
          
          private void FixedUpdate()
          {
               if(!_isInitialized) return;
               _vertexArray = _originalMesh.vertices;
               foreach (var t in _jellyVertices)
               {
                    var target = transform.TransformPoint(_vertexArray[t.ID]);
                    var bounds = _renderer.bounds;
                    var intensity = (1 - (bounds.max.y - target.y) / bounds.size.y) * _config.intensity;
                    t.Shake(target, _config.mass, _config.stiffness, _config.damping);
                    target = transform.InverseTransformPoint(t.position);
                    _vertexArray[t.ID] = Vector3.Lerp(_vertexArray[t.ID], target, intensity);
               }

               _clonedMesh.vertices = _vertexArray;
          }
     }
}