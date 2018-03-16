import * as React from "react";
import {IStaringUrlFormProps} from "./Interfaces";


export default class StaringUrlForm extends React.Component<IStaringUrlFormProps> {
    render() {
        
        return (
            <form>
                <fieldset disabled={this.props.isUrlProcessing}>
                    <div className="form-group">
                        <label htmlFor="urlInputText">Enter starting url:</label>
                        <input id="urlInputText"
                            type="text"
                            value={this.props.inputFormValue}
                            onChange={this.props.onChange}
                            className="form-control"
                            placeholder="http://company.com/"
                            autoFocus={true} />
                    </div>
                </fieldset>
                <input type="button"
                    value="Start"
                    className={this.props.isUrlProcessing ? "btn btn-primary hidden" : "btn btn-primary"}
                    onClick={this.props.onStartBtnClick} />

                <input type="button"
                    value="Stop"
                    className= {this.props.isUrlProcessing ? "btn btn-primary" : "btn btn-primary hidden"}
                    onClick={this.props.onStopBtnClick} />
            </form>
            );
    }
}
