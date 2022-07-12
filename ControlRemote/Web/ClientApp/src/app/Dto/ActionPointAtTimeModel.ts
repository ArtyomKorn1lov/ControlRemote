import { Byte } from "@angular/compiler/src/util";

export class ActionPointAtTimeModel {
    hourTimeAction: Date;
    flagImg: Byte;
    enableAction: boolean;

    public constructor(_hourTimeAction: Date, _flagImg: Byte, _enableAction: boolean) {
        this.hourTimeAction = _hourTimeAction;
        this.flagImg = _flagImg;
        this.enableAction = _enableAction;
    }
}