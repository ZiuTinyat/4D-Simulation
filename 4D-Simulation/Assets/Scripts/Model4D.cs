using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourthDimension {
    [Serializable]
    public struct Model4D {
        // Parsed data
        public string name;
        public List<List<float>> vertices_data;
        public List<List<int>> edges_data;
        public List<List<int>> polygons_data;
        public List<List<int>> volumes_data;

        // Useful data
        public List<Vector4> vertices;
        public List<Vector2Int> edges;
        public List<List<int>> polygons_edgeIndexed;
        public List<int> triangles_vertexIndexed;
        public List<List<int>> volumes_polygonIndexed;

        public void Init() {
            // vertices
            vertices = new List<Vector4>();
            foreach (var v in vertices_data) {
                vertices.Add(new Vector4(v[0], v[1], v[2], v[3]));
            }

            // edges
            edges = new List<Vector2Int>();
            foreach (var e in edges_data) {
                edges.Add(new Vector2Int(e[0], e[1]));
            }

            // polygons
            polygons_edgeIndexed = polygons_data;

            // triangles
            triangles_vertexIndexed = new List<int>();
            foreach (var p in polygons_data) {
                int start_vertex = edges[p[0]].x; // First vertex in first edge
                foreach (var ei in p) {
                    if (edges[ei].x != start_vertex && edges[ei].y != start_vertex) { // not adjacent edge
                        triangles_vertexIndexed.AddRange(new int[3] { start_vertex, edges[ei].x, edges[ei].y});
                    }
                }
            }

            // volumes
            volumes_polygonIndexed = volumes_data;
        }

        // Parse
        public static Model4D FromJson(string json) {
            return JsonConvert.DeserializeObject<Model4D>(json);
        }
    }
}
