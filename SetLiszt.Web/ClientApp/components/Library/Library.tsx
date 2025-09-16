import React, { JSX, useState, useEffect } from 'react';

import { Song, SongFile } from '../../types/entities';
import { SongListProps, SongViewerProps } from '../../types/componentProps';
import { fetchData } from '../../includes/utils';
import {
    URL_IMAGE_ROOT,
    URL_LIST_SONGS,
    URL_CHARTS_BASE,
    URL_UPLOAD_SONG
} from '../../includes/paths';

function songFileTransformer(data: any[]): SongFile[] {
    const songFiles: SongFile[] = data.map(d => {
        return {
            id: d.id,
            songId: d.songId,
            filepath: d.filepath,
            originalFilename: d.originalFileName,
            transposition: d.instrumentTransposition
        };
    });

    return songFiles;
}

function songTransformer(data: any[]): Song[] {
    const songs: Song[] = data.map(d => {
        return {
            id: Number(d.id),
            title: String(d.title),
            artist: d.artist ?? null,
            songFiles: songFileTransformer(d.songFiles)
        };
    });

    return songs;
}

function LibraryToolbar(): JSX.Element {
    return (
        <div className="library-toolbar w-100 d-flex flex-row justify-content-between align-items-center">
            <div className="toolbar-left text-small">
                <div className="antic-didone-regular">
                    <h4 className="m-0">Secret of the Forest</h4>
                    <span className="text-muted">Chrono Trigger</span>
                </div>
            </div>
            <div className="toolbar-right d-flex flex-row">
                {/* <a href="#" className="toolbar-button-round me-2">
                    <img src={`${URL_IMAGE_ROOT}/icons/more_light.svg`} alt="more icon" width="24px" className="me-0" />
                </a> */}
                <a href="#" className="toolbar-button-round me-2">
                    <img src={`${URL_IMAGE_ROOT}/icons/fullscreen_light.svg`} alt="fullscreen icon" width="24px" className="me-0" />
                </a>
                <a href="#" className="toolbar-button-round me-2">
                    <img src={`${URL_IMAGE_ROOT}/icons/edit_light.svg`} alt="edit song icon" width="24px" className="me-0" />
                </a>
                <a href={URL_UPLOAD_SONG} className="toolbar-button-round me-2">
                    <img src={`${URL_IMAGE_ROOT}/icons/add_light.svg`} alt="upload song icon" width="24px" className="me-0" />
                </a>
                <select className="form-select text-small" name="transpositionSelect" id="transpositionSelect">
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
                <label htmlFor="song-search-control">
                    <img src={`${URL_IMAGE_ROOT}/icons/search_light.svg`} width="24px" className="me-3" />
                    Search
                </label>
            </div>
        </div>
    );
}

function SongList({ songs, selectedSong, setSelectedSong }: SongListProps): JSX.Element {
    const baseClassName = "list-group-item list-group-item-action d-flex flex-column justify-content-between";
    
    return (
        <div className="list-group song-list-group list-group-flush">
            {songs.map(s => {
                const transpositions = s.songFiles.map(sf => sf.transposition).join(', ');
                let className = baseClassName;
                if (selectedSong && selectedSong.id === s.id) {
                    className += " active";
                }
                
                return (
                    <a
                        href="#"
                        className={className}
                        key={s.id}
                        onClick={(e) => { e.preventDefault(); setSelectedSong(s); }}
                    >
                        <div className="flex-row d-flex justify-content-between">
                            <div className="song-title"><strong>{s.title}</strong></div>
                            <div className="col instruments-markers d-flex justify-content-end text-end">
                                <small>{transpositions}</small>
                            </div>
                        </div>
                        <div className="flex-row justify-content-start">
                            <div className="song-artist text-muted">{s.artist}</div>
                        </div>
                    </a>
                );
            })}            
        </div>
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
                <SongListFilter />
                {loading ? <Loader /> : <SongList
                    songs={songs}
                    selectedSong={selectedSong}
                    setSelectedSong={setSelectedSong}
                />}
            </div>
            <div className="song-viewer-container w-75 d-flex flex-column">
                <LibraryToolbar />
                {<SongViewer song={selectedSong} />}
            </div>
        </div>
    );
}
