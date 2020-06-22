import { Sprite } from "../Sprite.js";
export interface IWavedShape extends Sprite {
    waveLength: number;
    waveHeight: number;
    tension: number;
}
