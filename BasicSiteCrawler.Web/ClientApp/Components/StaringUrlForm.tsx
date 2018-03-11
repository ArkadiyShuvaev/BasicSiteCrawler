import * as React from "react";
import {IStaringUrlFormProps} from "./Interfaces";


export default class StaringUrlForm extends React.Component<IStaringUrlFormProps> {
    render() {
        
        return (
            <form>
                <fieldset disabled={this.props.isInputDisabled}>
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
                    <input type="button"
                        value="Submit"
                        className="btn btn-primary"
                        onClick={this.props.onBtnClick} />
                </fieldset>
            </form>
            );
    }
}
