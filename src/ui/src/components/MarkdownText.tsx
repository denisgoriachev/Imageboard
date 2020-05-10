import React from 'react';
const ReactMarkdown = require('react-markdown');

interface IMarkdownTextProps {
    markdown?: string
}

export const MarkdownText: React.FC<IMarkdownTextProps> = (props) => {
    const disallowedTypes = ['image'];

    return <ReactMarkdown source={props.markdown} disallowedTypes={disallowedTypes} />
}