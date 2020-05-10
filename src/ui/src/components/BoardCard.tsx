import React from 'react';
import { BoardShortDto } from '../api/ImageboardClient';
import { Typography, Link } from '@material-ui/core';
import { Link as RouterLink } from 'react-router-dom';

type BoardCardProps = {
    data: BoardShortDto
}

export const BoardCard: React.FunctionComponent<BoardCardProps> = (props) => {
    return (
        <Link component={RouterLink} to={{ pathname: "/" + props.data.shortUrl, state: { title: props.data.title, description: props.data.description } }} >
            <Typography component="span">
                /{props.data.shortUrl} - {props.data.title}
            </Typography>
        </Link>
    )
}