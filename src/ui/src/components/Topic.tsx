import React, { useState, useEffect } from 'react';
import { TopicDto, TopicsClient } from '../api/ImageboardClient';
import { RouteComponentProps, Link as RouterLink } from 'react-router-dom';
import { Box, Breadcrumbs, Link, Typography, useTheme, Snackbar, Tooltip } from '@material-ui/core';
import { Dashboard } from '@material-ui/icons';
import { Skeleton, SpeedDial, SpeedDialIcon, Alert } from '@material-ui/lab';
import { Post } from './Post';
import { AddPostDialog } from './AddPostDialog';
import { ErrorAlert } from './ErrorAlert';
import usePolling from '../hooks/hooks';

interface TopicParams {
    id: string
}

export interface TopicLinkState {
    title?: string,
    boardShortUrl?: string,
    created?: boolean
}

interface TopicProps extends RouteComponentProps<TopicParams, any, TopicLinkState> {

}

export const Topic: React.FunctionComponent<TopicProps> = (props) => {

    const theme = useTheme();

    const [data, setData] = useState<TopicDto | null>(null);
    const [title, setTitle] = useState(props.location.state?.title);
    const [boardUrl, setBoardUrl] = useState(props.location.state?.boardShortUrl);
    const [created, setCreated] = useState(props.location.state?.created);

    const [postFormIsVisible, setPostFormIsVisible] = useState(false);
    const [newPostId, setNewPostId] = useState<number | undefined>(undefined);

    const [hasError, setHasError] = useState(false);

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const pollingData = usePolling<TopicDto | null>({ 
        delay: 5000, 
        fetchFunc: () => (new TopicsClient(process.env.REACT_APP_API_URL)).get(Number.parseInt(props.match.params.id)),
        initialState: null,
        onSuccess: setData,
        onError: () => setHasError(true)
     });

    useEffect(() => {
        const client = new TopicsClient(process.env.REACT_APP_API_URL);

        client.get(Number.parseInt(props.match.params.id))
            .then((data: TopicDto) => {
                setTitle(data.title);
                setBoardUrl(data.board?.shortUrl);
                setData(data);
            })
            .catch((error) => {
                setHasError(true);
            });
    }, [props.match.params.id, newPostId])

    useEffect(() => {
        document.title = (title == null ? "" : title + " - ") +
            (boardUrl == null ? "" : boardUrl + " - ") +
            "Imageboard"
    }, [title, boardUrl]);

    if(hasError) {
        return (
            <Box mt={3}>
                <ErrorAlert/>
            </Box>
        )
    }

    return (
        <Box>
            <Snackbar open={(created ?? false)} onClose={() => setCreated(false)} autoHideDuration={5000}>
                <Alert severity="success" onClose={() => setCreated(false)}>
                    Topic created!
                    </Alert>
            </Snackbar>

            <Snackbar open={(newPostId != null)} onClose={() => setNewPostId(undefined)} autoHideDuration={5000}>
                <Alert severity="success" onClose={() => setNewPostId(undefined)}>
                    Post added
                </Alert>
            </Snackbar>

            <Box mb={3} mt={2}>
                <Breadcrumbs aria-label="breadcrumb">
                    <Link color="inherit" style={{ display: 'flex' }} component={RouterLink} to="/" >
                        <Dashboard style={{ marginRight: 5, width: 20, height: 20 }} />
                            Boards
                        </Link>
                    <Link color="inherit" component={RouterLink} to={{ pathname: "/" + boardUrl, state: { title: data?.board?.title, description: data?.board?.description } }} >
                        <Typography>{boardUrl}</Typography>
                    </Link>
                    <Typography color="textPrimary" aria-current="page">{title}</Typography>
                </Breadcrumbs>
            </Box>

            <Box mb={3}>
                <Typography variant="h4">{title}</Typography>
            </Box>

            {
                data == null ?
                    <Box><Skeleton variant="text" height="200" /></Box> :
                    <Box mb={12} >
                        {
                            data?.posts?.map((data) => 
                                <Post onSubmitted={setNewPostId} key={data.id} deep={0} data={data} />)
                        }
                    </Box>
            }

            <Tooltip title="Reply" >
                <SpeedDial
                    style={{ position: "fixed", bottom: theme.spacing(3), right: theme.spacing(3) }}
                    ariaLabel="addPost"
                    open={false}
                    icon={<SpeedDialIcon />}
                    onClick={() => setPostFormIsVisible(true)}
                />
            </Tooltip>

            {postFormIsVisible ?
                <AddPostDialog
                    topicId={data?.id ?? -1}
                    open={true}
                    changeVisibilityHandler={setPostFormIsVisible}
                    onSubmitted={(postId) => { setNewPostId(postId); setPostFormIsVisible(false); }}
                    title={"Add new post to " + (data?.title ?? "")}
                /> :
                ""
            }
        </Box>
    )
}