using System.Collections;

namespace AdditionalTiers.Utils.Towers {
    internal sealed class TransformationManager : Instanced<TransformationManager> {
        private ArrayList TransformationList { get; } = new ArrayList();

        internal void Add(Transformation transformation) {
            if (string.IsNullOrWhiteSpace(transformation.Name))
                throw new ArgumentNullException(nameof(transformation.Name));

            TransformationList.Add(transformation);
            
            MelonDebug.Msg($"Added transformation to tower with ID {transformation.TowerID} named {transformation.Name}");
        }

        internal void Clear() {
            if (MelonDebug.IsEnabled())
                foreach (var obj in TransformationList)
                    MelonDebug.Msg($"Tower with ID {((Transformation) obj).TowerID} has been unregistered {((Transformation) obj).Name}");

            TransformationList.Clear();
        }

        internal Transformation Get(Tower tower) {
            for (var i = 0; i < TransformationList.Count; i++) {
                var transformation = (Transformation)TransformationList[i];
                if (transformation.TowerID == tower.Id.Id)
                    return transformation;
            }

            return new Transformation("", -1);
        }
        
        internal void Replace(Tower tower, Transformation transformation) {
            for (var i = 0; i < TransformationList.Count; i++) {
                if (tower.Id.Id == ((Transformation) TransformationList[i]).TowerID)
                    TransformationList[i] = transformation;
            }
        }
        
        internal void Remove(Tower tower) {
            var index = -1;
            for (var i = 0; i < TransformationList.Count; i++) {
                if (tower.Id.Id == ((Transformation) TransformationList[i]).TowerID)
                    index = i;
            }
            TransformationList.RemoveAt(index);
        }

        internal bool Contains(Tower tower) {
            for (var i = 0; i < TransformationList.Count; i++) {
                var transformation = (Transformation)TransformationList[i];
                if (transformation.TowerID == tower.Id.Id)
                    return true;
            }

            return false;
        }
    }

    internal struct Transformation {
        internal string Name {  get; set; }
        internal int TowerID { get; set; }

        internal Transformation(string name, int towerid) {
            Name = name;
            TowerID = towerid;
        }
    }
}
