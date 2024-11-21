using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

namespace ProceduralMeshes.Generators {

	public struct SharedTriangleGrid : IMeshGenerator {
        

		public int Resolution { get; set; }
        public int VertexCount => (Resolution + 1) * (Resolution + 1);

		public int IndexCount => 6 * Resolution * Resolution;

		public int JobLength => Resolution + 1;

		public Bounds Bounds => new Bounds(Vector3.zero, new Vector3(1f, 0f, 1f));


		public void Execute<S> (int z, S streams) where S : struct, IMeshStreams {

			int vi = (Resolution + 1) * z, ti = 2 * Resolution * (z - 1);

            float xOffset = -0.25f;
            float uOffset = 0f;

            if((z & 1) == 1) {
                xOffset = 0.25f;
                uOffset = 0.5f / (Resolution + 0.5f);
            }

            xOffset = xOffset / Resolution - 0.5f;

            var vertex = new Vertex();
            vertex.normal.y = 1f;
            vertex.tangent.xw = float2(1f, -1f);

            vertex.position.x = xOffset;
            vertex.position.z = ((float) z / Resolution - 0.5f) * sqrt(3f) / 2f;
            vertex.texCoord0.x = uOffset;
            vertex.texCoord0.y = vertex.position.z / (1f + 0.5f / Resolution) + 0.5f;
            streams.SetVertex(vi, vertex);

            vi += 1;

            for(int x = 1; x <= Resolution; x++, vi++, ti += 2)
            {
                vertex.position.x = (float) x / Resolution + xOffset;
                vertex.texCoord0.x = x / (Resolution + 0.5f) + uOffset;
                streams.SetVertex(vi, vertex);

                if (z > 0) {
					streams.SetTriangle(
						ti + 0, vi + int3(-Resolution - 2, -1, -Resolution - 1)
					);
					streams.SetTriangle(
						ti + 1, vi + int3(-Resolution - 1, -1, 0)
					);
				}
            }
		

		}
    }
}