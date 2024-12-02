using System.IO;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

namespace ProceduralMeshes.Generators {

	public struct Octasphere : IMeshGenerator {
		
		// static float3 CubeToSphere (float3 p) => p * sqrt (
		// 	1f - ((p * p).yxx + (p * p).zzy) / 2f + (p * p).yxx * (p*p).zzy / 3f
		// );
		static Rhombus GetRhombus(int id) => id switch {
			0 => new Rhombus {
				id = id,
				uvOrigin = -1f,
				uVector = 2f * right(),
				vVector = 2f * up(),
				seamStep = 4
			},
			1 => new Rhombus {
				id = id,
				uvOrigin = float3(1f, -1f, -1f),
				uVector = 2f * forward(),
				vVector = 2f * up(),
				seamStep = 4
			},
			2 => new Rhombus {
				id = id,
				uvOrigin = -1f,
				uVector = 2f * forward(),
				vVector = 2f * right(),
				seamStep = -2
			},
			3 => new Rhombus {
				id = id,
				uvOrigin = float3(-1f, -1f, 1f),
				uVector = 2f * up(),
				vVector = 2f * right(),
				seamStep = -2
			},
			4 => new Rhombus {
				id = id,
				uvOrigin = -1f,
				uVector = 2f * up(),
				vVector = 2f * forward(),
				seamStep = -2

			},
			_ => new Rhombus {
				id = id,
				uvOrigin = float3(-1f, 1f, -1f),
				uVector = 2f * right(),
				vVector = 2f * forward(),
				seamStep = -2
			}
		};

		struct Rhombus {
			public int id;
			public float3 uvOrigin, uVector, vVector;
			public int seamStep;

			public bool TouchesMinimumPole => (id & 1) == 0;
		}

		public int Resolution { get; set; }
        public int VertexCount => 4 * Resolution * Resolution + 2 * Resolution + 7;

		public int IndexCount => 6 * 4 * Resolution * Resolution;

		public int JobLength => 4 * Resolution + 1;

		public Bounds Bounds => new Bounds(Vector3.zero, new Vector3(2f, 2f, 2f));


		public void Execute<S> (int i, S streams) where S : struct, IMeshStreams {

			int u = i / 4;

			var rhombus = GetRhombus(i - 4 * u);

			int vi = Resolution * (Resolution * rhombus.id + u + 2) + 7;
			int ti = 2 * Resolution * (Resolution * rhombus.id + u);
			bool firstColumn = u == 0;
			u += 1;

			// float3 pStart = rhombus.uvOrigin + rhombus.uVector * u / Resolution;
			// float3 uB = Rhombus.uvOrigin + Rhombus.uVector * (u + 1) / Resolution;
			// float3 pA = CubeToSphere(uA), pB = CubeToSphere(uB);

			var vertex = new Vertex();

			// if(i == 0) {
			// 	vertex.position = -sqrt(1f / 3f);
			// 	streams.SetVertex(0, vertex);
			// 	vertex.position = sqrt(1f / 3f);
			// 	streams.SetVertex(1, vertex);
			// }
			// // vertex.tangent = float4(normalize(pB - pA), -1f);
			// vertex.position = CubeToSphere(pStart);
			streams.SetVertex(vi, vertex);

			// var triangle = int3(
			// 	vi,
			// 	firstColumn && rhombus.TouchesMinimumPole ? 0 : vi - Resolution,
			// 	vi + (firstColumn ?
			// 		rhombus.TouchesMinimumPole ?
			// 			rhombus.seamStep * Resolution * Resolution :
			// 			Resolution == 1 ? rhombus.seamStep : -Resolution + 1 :
			// 		-Resolution + 1
			// 	)
			// );
			// streams.SetTriangle(ti, triangle);
			// // streams.SetTriangle(ti + 0, 0);
			// // streams.SetTriangle(ti + 1, 0);
			vi += 1;
			// ti +=1;


			// int zAdd = firstColumn && rhombus.TouchesMinimumPole ? Resolution : 1;
			// int zAddLast = firstColumn && rhombus.TouchesMinimumPole ?
			// 	Resolution :
			// 	!firstColumn && !rhombus.TouchesMinimumPole ?
			// 		Resolution * ((rhombus.seamStep + 1) * Resolution - u) + u :
			// 		(rhombus.seamStep + 1) * Resolution * Resolution - Resolution + 1;

			for(int v = 1; v < Resolution; v++, vi ++, ti += 2) {
				
				// vertex.position = CubeToSphere(pStart + rhombus.vVector * v  / Resolution);
				streams.SetVertex(vi, vertex);
				// float3 pD = CubeToSphere(uB + Rhombus.vVector * v / Resolution);

				// var vertex = new Vertex();
				// vertex.normal = Rhombus.normal;
				// vertex.tangent = Rhombus.tangent;

				// vertex.position = pA;
				// // vertex.normal = normalize(cross(pC - pA, vertex.tangent.xyz));
				// // vertex.texCoord0 = 0f;
				// streams.SetVertex(vi + 0, vertex);

				// vertex.position = pB;
				// // vertex.normal = normalize(cross(pD - pB, vertex.tangent.xyz));
				// // vertex.texCoord0 = float2(1f, 0f);
				// streams.SetVertex(vi + 1, vertex);

				// vertex.position = pC;
				// // vertex.normal = pC;
				// // vertex.tangent.xyz = normalize(pD - pC);
				// // vertex.normal = normalize(cross(pC - pA, vertex.tangent.xyz));
				// // vertex.texCoord0 = float2(0f, 1f);
				// streams.SetVertex(vi + 2, vertex);

				// vertex.position = pD;
				// // vertex.normal = normalize(cross(pD - pB, vertex.tangent.xyz));
				// // vertex.texCoord0 = 1f;
				// streams.SetVertex(vi + 3, vertex);
				// triangle += 1;
				// triangle.x += 1;
				// triangle.y = triangle.z;
				// triangle.z += v == Resolution - 1 ? zAddLast : zAdd;
				// if(v == Resolution - 1) {
				// 	triangle.z += firstColumn && Rhombus.TouchesMinimumPole ?
				// 		Resolution :
				// 		!firstColumn && !Rhombus.TouchesMinimumPole ?
				// 			Resolution * ((Rhombus.seamStep + 1) * Resolution - u) + u :
				// 			(Rhombus.seamStep + 1) * Resolution * Resolution -
				// 			Resolution + 1;
				// }
				// else {
				// 	triangle.z += firstColumn && Rhombus.TouchesMinimumPole ? Resolution : 1;
				// }

				streams.SetTriangle(ti + 0, 0);
				streams.SetTriangle(ti + 1, 0);

				// pA = pC;
				// pB = pD;
			}

			streams.SetTriangle(ti + 0, 0);
			streams.SetTriangle(ti + 1, 0);

		}
    }
}