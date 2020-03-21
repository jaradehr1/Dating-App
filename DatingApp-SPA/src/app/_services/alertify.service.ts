import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})

export class AlertifyService {

  constructor() {

  }

  confirm(title: string, message: string, okCallback: () => any, cancelCallback: () => any, okText: string, cancelText: string) {
    alertify.confirm(title, message, () => { okCallback(); }
                , () => { cancelCallback(); }).set('labels', {ok: okText, cancel: cancelText});
  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  message(message: string) {
    alertify.message(message);
  }

  alert(title: string, message: string, okCallback: () => any, movable: boolean) {
    alertify.alert(title, message, () => { okCallback(); }).set('movable', movable);
  }

}
