import React, { JSX, useState, useEffect } from 'react';

import { Song } from '../../types/entities';
import { SongListProps, SongViewerProps } from '../../types/componentProps';
import { fetchData } from '../../includes/utils';
import {
    URL_LIST_SONGS,
    URL_CHARTS_BASE,
    URL_UPLOAD_SONG
} from '../../includes/paths';

function songTransformer(data: any[]): Song[] {
    const songs: Song[] = data.map(d => {
        return {
            id: Number(d.id),
            title: String(d.title),
            artist: d.artist ?? null,
            filepath: d.filePath ?? null,
        };
    });

    return songs;
}

function LibraryToolbar(): JSX.Element {
    return (
        <div className="library-toolbar border rounded w-100 p-2 d-flex flex-row justify-content-between align-items-center">
            <div className="toolbar-left">
                <a href={URL_UPLOAD_SONG} className="btn btn-sm btn-outline-primary">Upload</a>
            </div>
            <div className="toolbar-right">
                <select className="form-select" name="transpositionSelect" id="transpositionSelect">
                    <option value="0">Concert</option>
                    <option value="1">Bass</option>
                    <option value="2">Bb</option>
                    <option value="3">Eb</option>
                </select>
            </div>
        </div>
    );
}

function SongListFilter(): JSX.Element {
    return (
        <div className="song-search-wrapper">
            <div className="form-floating">
                <input
                    type="text"
                    className="form-control border border-0"
                    id="song-search-control"
                    placeholder="Search..."
                    style={{zIndex: 1, position: 'relative'}}
                />
                <label htmlFor="song-search-control">Search</label>
            </div>
        </div>
    );
}

function SongList({ songs }: SongListProps): JSX.Element {
    return (
        <ul className="list-group song-list-group list-group-flush">
            {songs.map(s => {
                return (
                    <li className="list-group-item d-flex flex-column justify-content-between" key={s.id}>
                        <div className="flex-row d-flex justify-content-start">
                            <div className="song-title"><strong>{s.title}</strong></div>
                            <div className="col instruments-markers d-flex justify-content-end">
                                <small>C, Bb, Eb</small>
                            </div>
                        </div>
                        <div className="flex-row justify-content-start">
                            <div className="song-artist text-muted">{s.artist}</div>
                        </div>
                    </li>
                );
            })}            
        </ul>
    );
}

function SongViewer({ song }: SongViewerProps): JSX.Element | null {
    // if (!song) {
    //     return null;
    // }

    return (
        <div className="song-viewer">
            <object
                data={URL_CHARTS_BASE + 'C - Secret of the Forest.pdf'}
                type="application/pdf"
                width={'100%'}
                height={'800px'}
            >
                <p><a href="#">Secret of the Forest</a></p>
            </object>
        </div>
    );
}

function Loader(): JSX.Element {
    return (
        <div className="loader">Loading songs...</div>
    );
}

export default function Library(): JSX.Element {
    const [songs, setSongs] = useState<Song[]>([]);
    const [selectedSong, setSelectedSong] = useState<Song | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchSongs();
    }, []);

    const fetchSongs = async () => {
        setLoading(true);

        try {
            const songList = await fetchData<Song>(URL_LIST_SONGS, songTransformer);
            setSongs([...songList]);
            setLoading(false);
        } catch (e) {
            console.error(e);
            setError('Unable to fetch song list. Please try again.');
            setLoading(false);
        }
    };

    return (
        <div className="library-wrapper w-100 d-flex flex-row justify-content-between container">
            <div className="song-list-container w-25">
                {<SongListFilter />}
                {loading ? <Loader /> : <SongList songs={songs} />}
            </div>
            <div className="song-viewer-container w-75 d-flex flex-column">
                <LibraryToolbar />
                {<SongViewer song={selectedSong} />}
            </div>
        </div>
    );
}
