import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import { Alert, AlertTitle } from '@material-ui/lab';
import { Link } from '@material-ui/core';

interface ErrorAlertProps {
    statusCode?: number
    header?: string | React.Component
}

export const ErrorAlert: React.FC<ErrorAlertProps> = (props) => {
    return (
        <Alert severity="error">
            <AlertTitle>{props.header ?? "Error"} {props.statusCode ?? ""}</AlertTitle>
            {props.children ??
                <div>
                    Unexpected error occured! Please, return to <Link color="inherit" underline="always" component={RouterLink} to="/" >home page</Link>
                </div>}
        </Alert>
    )
}