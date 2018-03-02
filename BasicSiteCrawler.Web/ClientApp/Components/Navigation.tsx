import * as React from "react";
import {INavigationProps} from "./Interfaces";


export default class Navigation extends React.Component<INavigationProps> {
    render() {
        return (
            <div>
                <a href="#" onClick={this.props.onBtnClick}>
                <img src="http://via.placeholder.com/550x50" /></a>
            </div>);
    }
}