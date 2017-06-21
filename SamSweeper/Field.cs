using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSweeper {
    class Field {

        public static readonly int[,] surroundingTiles = {
            {-1, -1}, {0, -1}, {1, -1},
            {-1,  0},          {1,  0},
            {-1,  1}, {0,  1}, {1,  1}
        };

        private int width;
        private int height;
        private int numOfMines;
        private bool isEmpty;

        private static Random rand = new Random();

        private Tile[,] tiles;

        public Field(int width, int height) {
            this.width = width;
            this.height = height;
            numOfMines = (width - 10) * (height - 10);

            isEmpty = true;

            tiles = new Tile[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tiles[x, y] = new Tile(x, y);
                }
            }
        }

        public void PlaceMines(int startTileX, int startTileY) {

            isEmpty = false;

            List<Tile> availableTiles = new List<Tile>();

            foreach (Tile tile in tiles) {
                availableTiles.Add(tile);
            }

            availableTiles.Remove(GetTile(startTileX, startTileY));

            for (int i = 0; i < 8; i++) {
                int x = startTileX + surroundingTiles[i, 0];
                int y = startTileY + surroundingTiles[i, 1];
                if (0 <= x && x < width && 0 <= y && y < height) {
                    availableTiles.Remove(GetTile(x, y));
                }
            }

            for (int i = 0; i < numOfMines; i++) {
                Tile tile = availableTiles[rand.Next(0, availableTiles.Count)];
                tile.IsBomb = true;
                availableTiles.Remove(tile);
            }

            //for (int i = 0; i < numOfMines; i++) {
            //    int x;
            //    int y;

            //    do {
            //        x = rand.Next(0, width);
            //        y = rand.Next(0, height);
            //    } while (IsMine(x, y) || (x == startTileX && y == startTileY));

            //    tiles[x, y].IsBomb = true;
            //}

            foreach (Tile tile in tiles) {
                for (int i = 0; i < 8; i++) {
                    if (IsMine(tile.X + surroundingTiles[i, 0], tile.Y + surroundingTiles[i, 1])) {
                        tile.NumSurroundingBombs++;
                    }
                }
            }
        }

        public void Reset() {
            isEmpty = true;
            foreach (Tile tile in tiles) {
                tile.IsBomb = false;
                tile.NumSurroundingBombs = 0;
            }
        }

        public bool IsMine(int x, int y) {
            if (0 <= x && x < width && 0 <= y && y < height) {
                return tiles[x, y].IsBomb;
            } else {
                return false;
            }
        }

        public Tile GetTile(int x, int y) {
            return tiles[x, y];
        }

        public bool IsEmpty {
            get {
                return isEmpty;
            }
        }
    }
}
