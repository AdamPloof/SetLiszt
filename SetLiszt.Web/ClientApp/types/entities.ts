export interface Song {
    id: number,
    title: string,
    artist: string | null,
    songFiles: Array<SongFile>,
}

export interface SongFile {
    id: number,
    songId: number,
    originalFilename: string,
    filepath: string,
    transposition: number
}
