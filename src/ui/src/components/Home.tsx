import React, { useState, useEffect } from 'react';
import { GroupDto, GroupsClient } from '../api/ImageboardClient';
import { BoardGroup } from './BoardGroup';
import { Box, Typography, Grid, Paper, List, ListItem, ListItemText } from '@material-ui/core';
import { Skeleton } from '@material-ui/lab';
import { ErrorAlert } from './ErrorAlert';

interface HomeSkeletonProps {
    count: number
}

const HomeSkeleton: React.FunctionComponent<HomeSkeletonProps> = (props) => {
    return (
        <List>
            {Array.from(new Array(props.count)).map((e, index) => (
                <ListItem key={index}>
                    <ListItemText primary={<Box key={index}>
                        <Skeleton variant="text" width={210} height={32} />
                        <Grid container spacing={3}>
                            <Grid item xs={6} md={3}>
                                <Skeleton variant="text" height={48} />
                            </Grid>
                            <Grid item xs={6} md={3}>
                                <Skeleton variant="text" height={48} />
                            </Grid>
                            <Grid item xs={6} md={3}>
                                <Skeleton variant="text" height={48} />
                            </Grid>
                        </Grid>
                    </Box>} />
                </ListItem>
            ))}
        </List>
    )
}

export const Home: React.FunctionComponent = () => {
    const [data, setData] = useState<GroupDto[] | null>(null);

    const [hasError, setHasError] = useState(false);

    useEffect(() => {
        const client = new GroupsClient(process.env.REACT_APP_API_URL);
        client.get()
            .then((data) => {
                setData(data)
            })
            .catch((error) => {
                setHasError(true);
            });
    }, []);

    document.title = "Home - Imageboard";

    if (hasError) {
        return (
            <Box mt={3}>
                <ErrorAlert />
            </Box>
        )
    }

    return (
        <Box>
            <Box mb={2} mt={2}>
                <Typography variant="h2">Welcome!</Typography>
            </Box>
            <Box mb={6}>
                <Paper>
                    {
                        data == null ?
                            <HomeSkeleton count={3} /> :
                            <List>
                                {
                                    data.map((group, index) => <ListItem key={index}>
                                        <ListItemText primary={<BoardGroup key={group.id} data={group} />} />
                                    </ListItem>)
                                }
                            </List>
                    }
                </Paper>
            </Box>
        </Box>
    )
}