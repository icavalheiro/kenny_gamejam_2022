public static class Tilesets
{
    public static readonly float TopOffset = 1;
    public static readonly float DiagonalOffset = 0.866f;

    public static readonly TilesetModel[] AvailableModels = new TilesetModel[] {
        new TilesetModel {
            Name = "dirt",
            ResourcePath = "res://Tilesets/dirt.tscn",
            PossibleNeighbours = new TilesetModel.PossibleNeighbour[] {
                new TilesetModel.PossibleNeighbour {
                    Name = "sand",
                    Probability = 0.2f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "grass",
                    Probability = 0.4f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "stone",
                    Probability = 0.3f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "forest",
                    Probability = 0.1f
                }
            }
        },
        new TilesetModel {
            Name = "forest",
            ResourcePath = "res://Tilesets/forest.tscn",
            PossibleNeighbours = new TilesetModel.PossibleNeighbour[] {
                new TilesetModel.PossibleNeighbour {
                    Name = "sand",
                    Probability = 0.1f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "grass",
                    Probability = 0.4f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "stone",
                    Probability = 0.3f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "water",
                    Probability = 0.1f
                }
            }
        },
        new TilesetModel {
            Name = "grass",
            ResourcePath = "res://Tilesets/grass.tscn",
            PossibleNeighbours = new TilesetModel.PossibleNeighbour[] {
                new TilesetModel.PossibleNeighbour {
                    Name = "sand",
                    Probability = 0.1f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "forest",
                    Probability = 0.4f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "stone",
                    Probability = 0.3f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "water",
                    Probability = 0.1f
                }
            }
        },
        new TilesetModel {
            Name = "sand",
            ResourcePath = "res://Tilesets/sand.tscn",
            PossibleNeighbours = new TilesetModel.PossibleNeighbour[] {
                new TilesetModel.PossibleNeighbour {
                    Name = "grass",
                    Probability = 0.1f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "stone",
                    Probability = 0.3f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "water",
                    Probability = 0.4f
                }
            }
        },
        new TilesetModel {
            Name = "stone",
            ResourcePath = "res://Tilesets/stone.tscn",
            PossibleNeighbours = new TilesetModel.PossibleNeighbour[] {
                new TilesetModel.PossibleNeighbour {
                    Name = "grass",
                    Probability = 0.2f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "forest",
                    Probability = 0.1f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "sand",
                    Probability = 0.4f
                },
                new TilesetModel.PossibleNeighbour {
                    Name = "dirt",
                    Probability = 0.3f
                }
            }
        },
        new TilesetModel {
            Name = "water",
            ResourcePath = "res://Tilesets/water.tscn",
            PossibleNeighbours = new TilesetModel.PossibleNeighbour[0]
        }
    };
}