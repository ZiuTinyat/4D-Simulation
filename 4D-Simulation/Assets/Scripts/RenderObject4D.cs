using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class RenderObject4D : MonoBehaviour {

        public Object4D renderObject;
        public ProjectionMode renderMode;

        private bool ortho { get { return renderMode == ProjectionMode.Orthogonal; } }
        private bool sliced { get { return renderMode == ProjectionMode.Sliced; } }
        private Model4D model { get { return renderObject.model; } }

        private Mesh mesh;

        private List<Vector4> transformedVertices;

        private List<Vector3> visualVertices;
        private List<List<int>> visualEdges;
        private List<int> visualTriangles;
        //private List<Vector3> visualNormals;

        private List<Vector3> slicedVertrices;
        private List<int> slicedTriangles;
        private List<Vector3> slicedNormals;
        public List<Vector3> stupidVertices;

        // Start is called before the first frame update
        void Start() {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            transformedVertices = new List<Vector4>();
            if (ortho) {
                visualVertices = new List<Vector3>(model.vertices.Count);
                visualTriangles = new List<int>();
                //mesh.triangles = model.triangles_vertexIndexed.ToArray();
            }
            if (sliced) {
                slicedVertrices = new List<Vector3>();
                slicedTriangles = new List<int>();
            }
        }

        // Update is called once per frame
        void Update() {
            // Toggle
            if (ortho) {
                GetComponent<MeshRenderer>().enabled = ObjectController.showOrtho;
            }
            if (sliced) {
                GetComponent<MeshRenderer>().enabled = ObjectController.showSliced;
            }

            // Transformed
            transformedVertices.Clear();
            foreach (var v in model.vertices) {
                transformedVertices.Add(renderObject.transform4D.LocalToWorldPosition(v));
            }

            if (ortho) {
                // Calculate vertices
                visualVertices.Clear();
                /*for (int i = 0; i < transformedVertices.Count; i++) {
                    Vector4 myWorldPos = transform.InverseTransformPoint(Manager4D.instance.ProjectToMyWorld(transformedVertices[i], ProjectionMode.Orthogonal));
                    visualVertices.Add(myWorldPos);
                }*/

                visualTriangles.Clear();
                int count = 0;
                foreach (var p in model.polygons_edgeIndexed) {
                    int start = model.edges[p[0]].x;
                    foreach (var ei in p) {
                        Vector2Int e = model.edges[ei];
                        if (e.x != start && e.y != start) {
                            visualVertices.Add(transform.InverseTransformPoint(Manager4D.instance.ProjectToMyWorld(transformedVertices[start], ProjectionMode.Orthogonal)));
                            visualVertices.Add(transform.InverseTransformPoint(Manager4D.instance.ProjectToMyWorld(transformedVertices[e.x], ProjectionMode.Orthogonal)));
                            visualVertices.Add(transform.InverseTransformPoint(Manager4D.instance.ProjectToMyWorld(transformedVertices[e.y], ProjectionMode.Orthogonal)));

                            visualTriangles.AddRange(new int[3] { count++, count++, count++ });
                        }
                    }
                }

                // Mesh
                mesh.triangles = null;
                mesh.SetVertices(visualVertices);
                mesh.SetTriangles(visualTriangles, 0);
                mesh.RecalculateNormals();
            }
            if (sliced) {
                List<bool> ei_valid = new List<bool>();

                // Slice edges to vertices
                slicedVertrices.Clear();
                foreach (var e in model.edges) {
                    Vector4 myWorldPos;
                    ei_valid.Add(Manager4D.instance.SliceEdge(transformedVertices[e.x], transformedVertices[e.y], out myWorldPos));
                    slicedVertrices.Add(transform.InverseTransformPoint(myWorldPos));
                }

                // sliced triangles
                int count = 0;
                stupidVertices = new List<Vector3>();
                slicedTriangles.Clear();
                foreach (var vo in model.volumes_polygonIndexed) {
                    List<Vector2Int> p_sliced = new List<Vector2Int>();
                    foreach (var pi in vo) {
                        List<int> edge_sliced = new List<int>();
                        foreach (var ei in model.polygons_edgeIndexed[pi]) {
                            if (ei_valid[ei]) edge_sliced.Add(ei);
                        }
                        if (edge_sliced.Count == 0) { // No slice

                        } else if (edge_sliced.Count == 1) { // "Edge" case
                            p_sliced.Add(new Vector2Int(edge_sliced[0], edge_sliced[0]));
                        } else { // count >= 2, should be 2 exactly
                            p_sliced.Add(new Vector2Int(edge_sliced[0], edge_sliced[1]));
                        }
                    }
                    if (p_sliced.Count >= 3) { // at least one triangle
                        int start = p_sliced[0].x;
                        foreach (var esi in p_sliced) {
                            if (esi.x != start && esi.y != start) {
                                //slicedTriangles.AddRange(new int[3] { start, esi.x, esi.y });
                                stupidVertices.Add(slicedVertrices[start]);
                                stupidVertices.Add(slicedVertrices[esi.x]);
                                stupidVertices.Add(slicedVertrices[esi.y]);
                                slicedTriangles.AddRange(new int[3] { count++, count++, count++ });
                            }
                        }
                    }
                }

                /*for (int i = 0; i < model.polygons.Count; i++) {
                    List<Vector4> worldFace = new List<Vector4>();
                    foreach (var v in model.polygons[i]) {
                        worldFace.Add(renderObject.transform4D.LocalToWorldPosition(model.vertices[v]));
                    }
                    List<Vector4> res;
                    if (Manager4D.instance.SliceFace(worldFace, out res)) {
                        for (int j = 0; j < res.Count; j += 2) {
                            int ind = slicedVertices.Count;
                            slicedVertices.Add(res[j]);
                            slicedVertices.Add(res[j + 1]);
                            slicedEdge.Add(new Vector2Int(ind, ind + 1));
                        }
                    }                    
                }*/

                // Mesh
                //Debug.Log(slicedEdge.Count);
                mesh.triangles = null;
                mesh.SetVertices(stupidVertices);
                mesh.SetTriangles(slicedTriangles, 0);
                mesh.RecalculateNormals();
            }
        }
        
    }
}
