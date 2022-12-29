export interface PASSWORD_VALIDATION_RULES {
    isLengthNoLongerThanMaxValue: boolean;
    isIncludeCapitalLetterSymbol: boolean;
    isIncludeNumber: boolean;
    isIncludeLowerCaseLetterSymbol: boolean;
    isMinLengthEnough: boolean;
}

export interface SIGN_UP_REQUEST_BODY {
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
}

export interface SIGN_IN_REQUEST_BODY {
    email: string;
    password: string;
}

export interface TOKEN_OBJ {
  accessToken: string;
}

export interface RESPONSE {
    message: string;
    isSuccess: boolean;
    Errors: string[];
    expireDate?: Date;
    user?: USER;
}

export interface USER {
    userName: string;
    email: string;
}
