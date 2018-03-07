import * as React from "react";
import {IStaringUrlFormProps} from "./Interfaces";


export default class StaringUrlForm extends React.Component<IStaringUrlFormProps> {
    render() {
        
        return (
            <form className="form-inline">
                <fieldset className="form-group" disabled={this.props.isInputDisabled}>
                    <label htmlFor="urlInputText">Enter starting url:</label>
                    <input id="urlInputText" type="text"
                        value={this.props.inputFormValue}
                        onChange={this.props.onChange}
                        className="form-control"
                        placeholder="http://company.com/"
                        autoFocus={true} />

                    <input type="button" value="Submit" className="btn btn-default" onClick={this.props.onBtnClick}/>
                </fieldset>
            </form>
            );
    }
}
