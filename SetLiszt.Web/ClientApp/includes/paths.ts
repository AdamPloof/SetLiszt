const base: string = process.env.BASE_URL ?? '';
export const URL_LIST_SONGS = base + "api/songs";
export const URL_GET_SONG_FILE = base + "api/songs/file";
export const URL_UPLOAD_SONG = base + "library/upload";
export const URL_IMAGE_ROOT = base + "images";
