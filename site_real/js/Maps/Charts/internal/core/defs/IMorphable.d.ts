import { Morpher } from "../utils/Morpher.js";
import { IPoint } from "./IPoint.js";
export interface IMorphable {
    morpher: Morpher;
    points: Array<Array<Array<IPoint>>>;
    currentPoints: Array<Array<Array<IPoint>>>;
}
