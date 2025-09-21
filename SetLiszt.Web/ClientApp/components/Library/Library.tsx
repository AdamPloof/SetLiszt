import React, { JSX, useState, useEffect } from 'react';

import { Song, SongFile } from '../../types/entities';
import {
    SongListProps,
    SongViewerProps,
    LibraryToolbarProps
} from '../../types/componentProps';
import { fetchData } from '../../includes/utils';
import {
    URL_IMAGE_ROOT,
    URL_LIST_SONGS,
    URL_GET_SONG_FILE,
    URL_UPLOAD_SONG
} from '../../includes/paths';
import { transpositions } from '../../includes/consts';

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

function LibraryToolbar({ song, handleChangeTransposition }: LibraryToolbarProps): JSX.Element | null {
    if (!song) {
        return null;
    }

    const availableTrans = song.songFiles.map(sf => sf.transposition);

    return (
        <div className="library-toolbar w-100 d-flex flex-row justify-content-between align-items-center">
            <div className="toolbar-left text-small">
                <div className="antic-didone-regular">
                    <h4 className="m-0">{song.title}</h4>
                    <span className="text-muted">{song.artist}</span>
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
                <select
                    className="form-select text-small"
                    name="transpositionSelect"
                    id="transpositionSelect"
                    onChange={(e) => {
                        handleChangeTransposition(e.target.value);
                    }}
                >
                    {Object.keys(transpositions).map(t => {
                        if (availableTrans.includes(t)) {
                            return (
                                <option
                                    key={`trs-${t}`}
                                    value={t}
                                >{t}</option>
                            );
                        }

                        return (
                            <option
                                key={`trs-${t}`}
                                value={t}
                                disabled
                            >{t}</option>
                        );
                    })}
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

function SongList({ songs, selectedSong, handleSelectSong }: SongListProps): JSX.Element {
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
                        onClick={(e) => { e.preventDefault(); handleSelectSong(s); }}
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

// TODO: let the user know when a song has available transpositions other than their preferred
// one if there is no songFile.
function SongViewer({ song, songFile }: SongViewerProps): JSX.Element | null {
    if (!song || !songFile) {
        return (
            <div className="song-viewer d-flex justify-content-center align-items-center p-5">
                <img src={`${URL_IMAGE_ROOT}/icons/music_note_light.svg`} alt="music note icon" width="24px" className="me-0" />
                <p className="text-muted mb-0 antic-didone-regular">Select song...</p>
            </div>
        );
    }

    return (
        <div className="song-viewer">
            <object
                data={`${URL_GET_SONG_FILE}/${song.id}/${songFile.transposition}`}
                type="application/pdf"
                width={'100%'}
                height={'800px'}
            >
                <p><a href="#">{song.title}</a></p>
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
    const [selectedSongFile, setSelectedSongFile] = useState<SongFile | null>(null);

    // TODO: define enum for transpositions
    const [selectedTransposition, setSelectedTransposition] = useState<string>("Concert");
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchSongs();
    }, []);

    /**
     * handleSelectSong takes care of updating the selected song as well as
     * choosing the appropriate song file based on the user's preferred transposition.
     * 
     * If a song file in user's preferred transposition is not available, then we'll
     * notify the user that there are other transpositions available (if there are any)
     * or otherwise let them know that there is no song file available for the song.
     * 
     * @param {Song} song 
     */
    const handleSelectSong = (song: Song): void => {
        setSelectedSong(song);

        const songFile: SongFile | undefined = song.songFiles.find(f => f.transposition === selectedTransposition);
        if (songFile) {
            setSelectedSongFile(songFile);
        }
    };

    const handleChangeTransposition = (transposition: string): void => {

    };

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
                    handleSelectSong={handleSelectSong}
                />}
            </div>
            <div className="song-viewer-container w-75 d-flex flex-column">
                {selectedSong ? <LibraryToolbar song={selectedSong} handleChangeTransposition={handleChangeTransposition} /> : null}
                <SongViewer song={selectedSong} songFile={selectedSongFile} />
            </div>
        </div>
    );
}
