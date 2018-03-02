import * as React from "react";
import { IUrlListRowProps } from "./Interfaces";

export default class UrlListRow extends React.PureComponent<IUrlListRowProps> {
    constructor(props: IUrlListRowProps) {
        super(props);
    }

    render() {
        return <div>{this.props.url}</div>;
    }
}
