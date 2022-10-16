namespace AdditionalTiers.Utils.Attack {
    public static class OrbitModelUtil {
        public static List<OrbitModel> CloneOrbit(this OrbitModel orbit, int count, int startAmount, int endAmount, int spaceBetween) {
            var orbits = new List<OrbitModel>();

            for (int i = 0; i < count; i++) {
                var tmpOrbit = orbit.CloneCast();
                tmpOrbit.name = $"Orbit_Gen_{i}";
                tmpOrbit.count = (endAmount - startAmount) / count * i + startAmount;
                tmpOrbit.range = spaceBetween * (i + 1);
                orbits.Add(tmpOrbit);
            }

            return orbits;
        }
    }
}
