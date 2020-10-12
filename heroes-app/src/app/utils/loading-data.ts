export abstract class LoadingData {
    private _isLoading: boolean = false;

    get isLoading(): boolean {
        return this._isLoading;
    }

    public stopLoading() {
        this._isLoading = false;
    }

    public startLoading() {
        this._isLoading = true;
    }
}
