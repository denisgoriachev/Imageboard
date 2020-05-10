import React from 'react';
import { GroupDto } from '../api/ImageboardClient';
import { Typography, Grid, Box, makeStyles } from '@material-ui/core';
import { BoardCard } from './BoardCard';

type BoardGroupProps = {
    data: GroupDto
}

const useStyles = makeStyles((theme) => ({
    boardGroup: {
        marginBottom: theme.spacing(3)
    },
    boardGroupTitle: {
        marginBottom: theme.spacing(2)
    }
}));

export const BoardGroup: React.FunctionComponent<BoardGroupProps> = (props) => {
    const classes = useStyles();

    return (
        <Box className={classes.boardGroup}>
            <Typography className={classes.boardGroupTitle} variant="h5">{props.data.title}</Typography>
            <Box>
                <Grid container spacing={3} alignItems="stretch">
                    {props.data.boards?.map((boardDto) => <Grid key={boardDto.id} item xs={12} sm={6} md={4} lg={3}><BoardCard data={boardDto} /></Grid>)}
                </Grid>
            </Box>
        </Box>
    )
}