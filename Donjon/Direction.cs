namespace Donjon {
    static class Direction {
        public static Position N => new Position(x: 0,  y: -1);
        public static Position S => new Position(x: 0,  y: +1);
        public static Position W => new Position(x: -1, y: 0);
        public static Position E => new Position(x: +1, y: 0);
    }
}