import { ActionPointAtTimeModel } from "./ActionPointAtTimeModel";

export class ActionSortByDateTimeModel {
    dateTimeAction: Date;
    commands: ActionPointAtTimeModel[];

    public constructor(_dateTimeAction: Date, _commands: ActionPointAtTimeModel[]) {
        this.dateTimeAction = _dateTimeAction;
        this.commands = _commands;
    }
}