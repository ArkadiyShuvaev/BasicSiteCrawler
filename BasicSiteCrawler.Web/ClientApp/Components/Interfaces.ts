export interface IFormData {
    inputFormValue: string;
    isInputDisabled: boolean;
}

export interface IAppState {
    urls: Array<IUrl>;
    formData: IFormData;
}

export interface IStaringUrlFormProps {
    inputFormValue: string;
    isInputDisabled: boolean;
    onChange: (e: React.FormEvent<HTMLInputElement>) => void;
    onBtnClick: (e: React.FormEvent<HTMLInputElement>) => void;
}

export interface IUrlListRowProps {
    url: string;
}

export interface IUrlListProps {
    rows: Array<IUrl>
}

export interface IUrl {
    url: string;
    id: string;
}