import React, { useState } from 'react';
import ReactMde from "react-mde";
import "react-mde/lib/styles/css/react-mde-toolbar.css";
import "react-mde/lib/styles/css/react-mde-editor.css";
import "react-mde/lib/styles/css/react-mde.css";

import { Typography } from '@material-ui/core';
import { MarkdownText } from './MarkdownText';

interface ITextEditorProps {
    name?: string,
    required?: boolean,
    label?: string,
    value?: string,
    errors?: string,
    error?: boolean,
    onChange: {
        (e: React.ChangeEvent<any>): void;
        <T = string | React.ChangeEvent<any>>(field: T): T extends React.ChangeEvent<any> ? void : (e: string | React.ChangeEvent<any>) => void;
    }
    onBlur?: {
        (e: React.FocusEvent<any>): void;
        <T = any>(fieldOrEvent: T): T extends string ? (e: any) => void : void;
    }
}

export const TextEditor: React.FC<ITextEditorProps> = (props) => {
    const [value, setValue] = useState(props.value);
    const [selectedTab, setSelectedTab] = useState<"write" | "preview">(
        "write"
    );

    const toolbarCommands = [
        ['header', 'bold', 'italic', 'strikethrough'],
        ['link', 'quote', 'code'],
        ['unordered-list', 'ordered-list', 'checked-list']];

    return (
        <React.Fragment>
            <ReactMde
                value={value}
                selectedTab={selectedTab}
                onTabChange={setSelectedTab}
                generateMarkdownPreview={(markdown) =>
                    Promise.resolve(<MarkdownText markdown={markdown} />)
                }
                toolbarCommands={toolbarCommands}
                childProps={{
                    writeButton: {
                        tabIndex: -1
                    },
                    textArea: {
                        name: props.name,
                        onChange: (event) => { setValue(event.target.value); props.onChange(event); },
                        onBlur: props.onBlur
                    }
                }}
            />
            {(props.error ?? false) ? <Typography variant="caption" color="error">{props.errors}</Typography> : ""}
        </React.Fragment>

    )
}