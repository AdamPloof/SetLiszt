import React from "react";
import { Song } from "./entities"

export interface SongListProps {
    songs: Song[];
    selectedSong: Song | null;
    setSelectedSong: React.Dispatch<React.SetStateAction<Song | null>>
}

export interface SongViewerProps {
    song: Song | null;
}
