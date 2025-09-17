import React from "react";
import { Song, SongFile } from "./entities"

export interface SongListProps {
    songs: Song[];
    selectedSong: Song | null;
    handleSelectSong: (song: Song) => void;
}

export interface SongViewerProps {
    song: Song | null;
    songFile: SongFile | null;
}

export interface LibraryToolbarProps {
    song: Song | null;
    handleChangeTransposition: (transposition: string) => void;
}
