using Kyzlyk.Helpers.GMesh;

namespace Kyzlyk.LifeSupportModules.Breaking.Modules
{
    public struct Decoration
    {
        public Decoration(MeshStructure meshStructure)
        {
            MeshStructure = meshStructure;
        }

        public MeshStructure MeshStructure;
        
        public bool CheckAccessToMesh()
        {
            return MeshStructure.Vertices != null && MeshStructure.Triangles != null && MeshStructure.UVs != null && MeshStructure.Vertices.Count > 0 && MeshStructure.Triangles.Count > 0 && MeshStructure.Vertices.Count == MeshStructure.UVs.Count;
        }
    }
}