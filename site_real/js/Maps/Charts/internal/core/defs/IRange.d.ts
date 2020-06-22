import { Optional } from "../utils/Type.js";
export interface IRange {
    start: Optional<number>;
    end: Optional<number>;
    priority?: "start" | "end";
}
