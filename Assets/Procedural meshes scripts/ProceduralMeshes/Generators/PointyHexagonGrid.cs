using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

namespace ProceduralMeshes.Generators {

	public struct PointyHexagonGrid : IMeshGenerator {
        

		public int Resolution { get; set; }
        public int VertexCount => 7 * Resolution * Resolution;

		public int IndexCount => 18 * Resolution * Resolution;

		public int JobLength => Resolution;

		public Bounds Bounds => new Bounds(Vector3.zero, new Vector3(1f, 0f, 1f));


		public void Execute<S> (int z, S streams) where S : struct, IMeshStreams {

			int vi = 7 * Resolution * z, ti = 6 * Resolution * z;

			float h = sqrt(3f) / 4f;
			
			for(int x = 0; x < Resolution; x++, vi += 7, ti += 6) {
				var xCoordinates = float2(-h, h) / Resolution;
				var zCoordinates = float4(-0.5f, -0.25f, 0.25f, 0.5f) / Resolution;

				var vertex = new Vertex();
				vertex.normal.y = 1f;
				vertex.tangent.xw = float2(1f, -1f);

				vertex.texCoord0 = 0.5f;
				streams.SetVertex(vi + 0, vertex);

				vertex.position.z = zCoordinates.x;
				vertex.texCoord0.y = 0f;
				streams.SetVertex(vi + 1, vertex);

				vertex.position.x = xCoordinates.x;
				vertex.position.z = zCoordinates.y;
				vertex.texCoord0 = float2(0.5f - h, 0.25f);
				streams.SetVertex(vi + 2, vertex);

				vertex.position.z = zCoordinates.z;
				vertex.texCoord0.y = 0.75f; 
				streams.SetVertex(vi + 3, vertex);

				vertex.position.x = 0f;
				vertex.position.z = zCoordinates.w;
				vertex.texCoord0 = float2(0.5f, 1f);
				streams.SetVertex(vi + 4, vertex);

				vertex.position.x = xCoordinates.y;
				vertex.position.z = zCoordinates.z;
				vertex.texCoord0 = float2(0.5f + h, 0.75f);
				streams.SetVertex(vi + 5, vertex);

				vertex.position.z = zCoordinates.y;
				vertex.texCoord0.y = 0.25f;
				streams.SetVertex(vi + 6, vertex);


				streams.SetTriangle(ti + 0, vi + int3(0, 1, 2));
				streams.SetTriangle(ti + 1, vi + int3(0, 2, 3));
				streams.SetTriangle(ti + 2, vi + int3(0, 3, 4));
				streams.SetTriangle(ti + 3, vi + int3(0, 4, 5));
				streams.SetTriangle(ti + 4, vi + int3(0, 5, 6));
				streams.SetTriangle(ti + 5, vi + int3(0, 6, 1));



				// var xCoordinates = float2(x, x + 1f) / Resolution - 0.5f;
				// var zCoordinates = float2(z, z + 1f) / Resolution - 0.5f;

				// var vertex = new Vertex();
				// vertex.normal.y = 1f;
				// vertex.tangent.xw = float2(1f, -1f);

				// vertex.position.x = xCoordinates.x;
				// vertex.position.z = zCoordinates.x;
				// streams.SetVertex(vi + 0, vertex);

				// vertex.position.x = xCoordinates.y;
				// vertex.texCoord0 = float2(1f, 0f);
				// streams.SetVertex(vi + 1, vertex);

				// vertex.position.x = xCoordinates.x;
				// vertex.position.z = zCoordinates.y;
				// vertex.texCoord0 = float2(0f, 1f);
				// streams.SetVertex(vi + 2, vertex);

				// vertex.position.x = xCoordinates.y;
				// vertex.texCoord0 = 1f;
				// streams.SetVertex(vi + 3, vertex);
				
				// streams.SetTriangle(ti + 0, vi + int3(0, 2, 1));
				// streams.SetTriangle(ti + 1, vi + int3(1, 2, 3));
			}

		}
    }
}