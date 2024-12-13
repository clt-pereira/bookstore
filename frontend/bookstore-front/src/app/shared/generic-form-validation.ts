import { FormGroup, AbstractControl } from '@angular/forms';

export class GenericValidator {
  constructor(private validationMessages: ValidationMessages) {}

  processarMensagens(container: FormGroup): DisplayMessage {
    const messages: DisplayMessage = {};
    this.verificarControles(container, messages);
    return messages;
  }

  private verificarControles(container: FormGroup, messages: DisplayMessage): void {
    Object.entries(container.controls).forEach(([controlKey, control]) => {
      if (control instanceof FormGroup) {
        this.verificarControles(control, messages);
      } else {
        this.atualizarMensagens(controlKey, control, messages);
      }
    });
  }

  private atualizarMensagens(controlKey: string, control: AbstractControl, messages: DisplayMessage): void {
    const controlMessages = this.validationMessages[controlKey];
    if (controlMessages && (control.dirty || control.touched) && control.errors) {
      messages[controlKey] = Object.keys(control.errors)
        .filter((errorKey) => controlMessages[errorKey])
        .map((errorKey) => controlMessages[errorKey])
        .join('<br />');
    } else {
      messages[controlKey] = '';
    }
  }
}

export interface DisplayMessage {
  [key: string]: string;
}

export interface ValidationMessages {
  [key: string]: { [key: string]: string };
}
