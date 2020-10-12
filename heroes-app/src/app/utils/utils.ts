import { ModalOptions } from 'ngx-bootstrap/modal';
import { LoadingData } from './loading-data';

export abstract class Utils extends LoadingData {
    public getModalConfig(cssModal: string): ModalOptions {
        return Object.assign({}, { class: cssModal});
    }
}
