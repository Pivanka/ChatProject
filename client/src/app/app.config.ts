import { PASSWORD_VALIDATION_RULES } from './models/authorization.models';

import { FormControl, FormRecord, ValidatorFn } from '@angular/forms';

export const PasswordRulesStartValue: PASSWORD_VALIDATION_RULES = {
  isLengthNoLongerThanMaxValue: false,
  isIncludeCapitalLetterSymbol: false,
  isIncludeNumber: false,
  isIncludeLowerCaseLetterSymbol: false,
  isMinLengthEnough: false,
}

export const passwordPattern = '^(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[a-z]).{4,8}$';

export function match(passControl1: FormControl, passControl2: FormControl): ValidatorFn{
    return () => {
        if(!passControl1.value || !passControl2.value) return null;
        if(passControl1.value === passControl2.value) return null;
        else { return {match} }
    }
}
